using Core.Logging.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging
{
    public class Logger
    {
        public string Source { get; }
        public string? Unit { get; private set; }

        public Logger(string source, string? unit = null)
        {
            Source = source;
            Unit = unit;
        }

        public void Log(LogLevel level, LogCategory category, string message, Exception? exception = null)
        {
            if (!LogService.IsEnabled(level))
            {
                return;
            }

            var thread = Thread.CurrentThread;
            var entry = new LogEntry(DateTime.Now, level, category, Source, message, thread.Name, exception);

            LogService.Write(entry);

        }

        public void Trace(string message)
        {
            Log(LogLevel.Trace, LogCategory.System, message);
        }

        public void Debug(string message)
        {
            Log(LogLevel.Debug, LogCategory.System, message);
        }
        public void Info(string message) => Log(LogLevel.Info, LogCategory.System, message);
        public void Warn(string message) => Log(LogLevel.Warn, LogCategory.System, message);

        public void Error(string message, Exception? ex = null) => Log(LogLevel.Error, LogCategory.System, message, ex);
        public void Fatal(string message, Exception? ex = null) => Log(LogLevel.Fatal, LogCategory.System, message, ex);
        // 작업자 조작
        public void Event(string message) => Log(LogLevel.Info, LogCategory.Event, message);


        public void Seq(string message)=>Log(LogLevel.Info, LogCategory.Sequence, message);
        public void Comm(string message)=>Log(LogLevel.Debug, LogCategory.Comm, message);

    }







}
