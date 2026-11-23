using Core.Logging.Targets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging.Service
{
    public static class LogService
    {
        private static ILogTarget[] _targets = Array.Empty<ILogTarget>();

        private static readonly ConcurrentDictionary<string, Logger> _loggers = new();
        public static LogLevel MinLevel { get; private set; }

        public static void Configure(LogLevel minLevel, params ILogTarget[] targets)
        {
            MinLevel = minLevel;
            _targets = targets;
        }

        public static Logger GetLogger(string source, string? unit = null)
        {
            string key = unit is null ? source : $"{source}|{unit}";
            return _loggers.GetOrAdd(key, _=>new Logger(source, unit));
            //return _loggers.GetOrAdd(source, s=>new Logger(s));

        }

        public static Logger GetLogger<T>(string? unit = null)
        {
            return GetLogger(typeof(T).Name, unit);
        }
        public static void Shutdown()
        {
            var targets = _targets;
            _targets = Array.Empty<ILogTarget>();   // 이후 로그는 무시

            foreach (var target in targets)
            {
                if (target is IDisposable disposable)
                {
                    try { disposable.Dispose(); }
                    catch { /* 종료 중 실패는 무시 */ }
                }
            }
        }
        internal static bool IsEnabled(LogLevel level)
        {
            return level >= MinLevel;
        }

        internal static void Write(LogEntry entry)
        {
            var targets = _targets;

            foreach (var target in targets)
            {
                try
                {
                    target.Write(entry);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Log Fail] {target.GetType().Name}: {ex.Message}");
                }

            }

        }



    }
}
