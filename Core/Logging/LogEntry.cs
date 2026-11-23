using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging
{
    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
    public enum LogCategory
    {
        System,
        Event,
        Alarm,
        Sequence,
        Comm,
    }

    public class LogScope
    {
        public string Key { get; }
        public string Value { get; }
        public LogScope? Parent { get; }

        internal LogScope(string key, string value, LogScope? parent)
        {
            Key = key;
            Value = value;
            Parent = parent;
        }
    }

    public static class LogContext
    {
        private static readonly AsyncLocal<LogScope?> _current = new();
        public static LogScope? Current => _current.Value;
        public static IDisposable Push(string key, object? value)
        {
            var parent = _current.Value;
            _current.Value = new LogScope(key, value?.ToString() ?? "null", parent);
            return new Pop(parent);
        }
        private class Pop : IDisposable
        {
            private readonly LogScope? _restore;
            private bool _disposed;

            public Pop(LogScope? restore) => _restore = restore;

            public void Dispose()
            {
                if (_disposed) return;     // 두 번 Dispose해도 안전
                _disposed = true;
                _current.Value = _restore;
            }
        }
    }

    public record class LogEntry(DateTime Time, LogLevel Level, LogCategory Category,
        string Source, string Message, 
        string? ThreadName, Exception? Exception,
        string? Unit = null);

    public static class LogFormatter
    {
        public static string Format(LogEntry entry)
        {
            string thread = entry.ThreadName ?? "anonymous";

            string line = $"{entry.Time:yyyy-MM-dd HH:mm:ss.fff} " +
                          $"[{LevelText(entry.Level)}] " +
                          $"[{entry.Category,-8}] " +
                          $"[T:{thread}] " +
                          $"[{entry.Source}] {entry.Message}";
                          //$"[{entry.Source}] {FormatScope(entry.Scope)} {entry.Message}";
            return entry.Exception is null ? line : line + Environment.NewLine + entry.Exception;
        }

        private static string FormatScope(LogScope? scope)
        {
            if (scope is null) return "";

            // 노드는 안쪽(최근) → 바깥 순서로 연결돼 있어서, 뒤집어서 바깥(LOT)부터 출력
            var stack = new Stack<LogScope>();
            for (var s = scope; s is not null; s = s.Parent)
                stack.Push(s);

            var sb = new StringBuilder(" {");
            bool first = true;
            while (stack.Count > 0)
            {
                var s = stack.Pop();
                if (!first) sb.Append(' ');
                sb.Append(s.Key).Append('=').Append(s.Value);
                first = false;
            }
            return sb.Append('}').ToString();
        }

        public static string LevelText(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return "TRACE";
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Info:
                    return "Info ";
                case LogLevel.Warn:
                    return "Warn ";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Fatal:
                    return "Fatal";
                default:
                    return "?????";
            }
        }
    }
}
