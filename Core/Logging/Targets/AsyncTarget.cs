using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Core.Logging.Targets
{
    public class AsyncTarget : ILogTarget, IDisposable
    {

        private readonly ILogTarget _target;
        private readonly BlockingCollection<LogEntry> _queue;
        private readonly Thread _worker;
        private readonly int _batchSize;
        private readonly TimeSpan _shutdownTimeout;

        private int _droppedCount;

        public AsyncTarget(ILogTarget target, int capacity = 10000, int batchSize = 200, TimeSpan? shutdownTimeout = null)
        {

            _target = target;
            _batchSize = batchSize;
            _shutdownTimeout = shutdownTimeout ?? TimeSpan.FromSeconds(3);
            
            _queue = new BlockingCollection<LogEntry>(capacity);

            _worker = new Thread(ProcessLoop)
            {
                IsBackground = true,
                Name = "LogWriter"
            };
            _worker.Start();


        }

        private void ProcessLoop()
        {
            var batch = new List<LogEntry>(_batchSize);

            while(_queue.TryTake(out var first, Timeout.Infinite))
            {
                batch.Add(first);
                while (batch.Count < _batchSize && _queue.TryTake(out var next))
                {
                    batch.Add(next);
                }
                WriteToInner(batch);
                batch.Clear();

            }
        }

        private void WriteToInner(List<LogEntry> batch)
        {
            try
            {
                if (_target is IBatchLogTarget batchTarget)
                {
                    batchTarget.WriteBatch(batch);
                }
                else
                {
                    foreach (var entry in batch)
                        _target.Write(entry);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Log Fail] {_target.GetType().Name}, {ex.Message}");
            }
        }


        public void Write(LogEntry entry)
        {

            try
            {

                while (!_queue.TryAdd(entry))
                {
                    if (_queue.TryTake(out _))
                    {
                        Interlocked.Increment(ref _droppedCount);
                    }
                }
            }
            catch (InvalidOperationException)
            {

            }
            catch(Exception)
            { }
 
        }
        public void Dispose()
        {
            _queue.CompleteAdding();
            bool finished = _worker.Join(_shutdownTimeout);
            if (finished)
            {
                _queue.Dispose();
            }
            else
            {

            }

            if(_target is IDisposable disposable)
                disposable.Dispose();
        }

    }
}
