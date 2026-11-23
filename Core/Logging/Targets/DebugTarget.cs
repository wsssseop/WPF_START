using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging.Targets
{

    public class DebugOutputTarget : ILogTarget
    {
        public void Write(LogEntry entry)
        {
            Debug.WriteLine(LogFormatter.Format(entry));
        }
    }
}
