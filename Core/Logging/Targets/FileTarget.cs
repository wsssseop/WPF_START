using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging.Targets
{
    public record OpenFile(string Path, StreamWriter Writer);

    public class FileTarget : IBatchLogTarget, IDisposable
    {
        private readonly string _rootFolder;
        private readonly object _lock = new object();
        private readonly Dictionary<string, OpenFile> _openFiles = new();


        public FileTarget(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        public void Write(LogEntry entry)
        {
            //string path = GetFilePath(entry);
            //string line = LogFormatter.Format(entry);

            //lock(_lock)
            //{
            //    var writer = GetWriter(entry.Category, path);
            //    writer.WriteLine(line);
            //    writer.Flush();
            //}

            WriteBatch(new[] { entry });

        }

        private StreamWriter GetWriter(string fileKey, string path)
        {
            if (_openFiles.TryGetValue(fileKey, out var current))
            {
                if (current.Path == path)
                {
                    return current.Writer;
                }
                current.Writer.Dispose();
                _openFiles.Remove(fileKey);
            
            }
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            var writer = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));

            _openFiles[fileKey] = new OpenFile(path, writer);
            return writer;
        }

        private static string GetFileKey(LogEntry e)
        {
            bool split = e.Unit is not null && (e.Category is LogCategory.Sequence or LogCategory.Comm);
            return split ? $"{e.Category}_{e.Unit}" : e.Category.ToString();
        }

        private string GetFilePath(LogEntry entry, string fileKey)
        {
            var t = entry.Time;
            return Path.Combine(_rootFolder, t.ToString("yyyy"), t.ToString("MM"), t.ToString("dd"), $"{fileKey}.log");

        }

        public void Dispose()
        {
            lock (_lock)
            {
                foreach (var file in _openFiles.Values)
                    file.Writer.Dispose();

                _openFiles.Clear();
            }

        }


        public void WriteBatch(IReadOnlyList<LogEntry> entries)
        {
            //var lines = new List<(LogCategory, string, string)> ();


            var lines = new (string Key, string Path, string Line)[entries.Count];

            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                string key = GetFileKey(e);
                lines[i] = (key, GetFilePath(e, key), LogFormatter.Format(e));
            }

            lock (_lock)
            {
                foreach (var (key, path, line) in lines)
                    GetWriter(key, path).WriteLine(line);

                foreach(var file in _openFiles.Values)
                    file.Writer.Flush();

            }


        }
    }
}
