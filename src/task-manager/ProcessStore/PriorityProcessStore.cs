using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace task_manager.ProcessStore
{
    public class PriorityProcessStore : IProcessStore
    {
        private int Size { get; }
        private readonly List<Process> _processes = new List<Process>();

        public PriorityProcessStore(int capacity)
        {
            Size = capacity;
        }

        /// <summary>
        /// when the max size is reached, should result into an evaluation: if the new
        /// process passed in the add() call has a higher priority compared to any of
        /// the existing one, we remove the lowest priority that is the oldest, otherwise we skip it
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public bool Add(Process process)
        {
            if (_processes.Count == Size)
            {
                var old = _processes
                    .OrderByDescending(x => x.Priority)
                    .ThenBy(x => x.CreatedAt)
                    .First();

                if (old.Priority > process.Priority)
                {
                    _processes.Remove(old);
                    old.Kill();
                    _processes.Add(process);
                    return true;
                }

                return false;
            }

            _processes.Add(process);
            return true;
        }

        public Process Get(string pid) => _processes.SingleOrDefault(x => x.PID == pid);
        public IEnumerable<Process> GetAll() => _processes.ToList();
    }
}