using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging.Targets
{
    public interface ILogTarget
    {
        void Write(LogEntry entry);
    }

    public interface IBatchLogTarget : ILogTarget
    {
        void WriteBatch(IReadOnlyList<LogEntry> entries);
    }

}
