using System;
using System.Collections.Generic;
using System.Text;

namespace task_manager
{
    /// <summary>
    /// process is identified by 2 fields, a unique unmodifiable identifier(PID), and a priority(low, medium, high).
    /// The process is immutable, it is generated with a priority and will die with this priority
    /// – each process has a kill() method that will destroy it
    /// </summary>
    public class Process
    {
        public string PID { get; }
        public ProcessPriority Priority { get; }
        public DateTime CreatedAt { get; }
        public bool Running { get; private set; }

        public Process(string pid, ProcessPriority priority = ProcessPriority.Low)
        {
            PID = pid ?? throw new ArgumentNullException(nameof(pid));
            Priority = priority;
            Running = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Kill()
        {
            Running = false;
        }
    }

    public enum ProcessPriority
    {
        High = 0,
        Medium = 1,
        Low = 2,
    }
}