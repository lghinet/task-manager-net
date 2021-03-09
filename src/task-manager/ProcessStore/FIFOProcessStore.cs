using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace task_manager.ProcessStore
{
    public class FIFOProcessStore : IProcessStore
    {
        private int Size { get; }
        private readonly Queue<Process> _queue = new Queue<Process>();

        public FIFOProcessStore(int capacity)
        {
            Size = capacity;
        }

        /// <summary>
        /// accept all new processes through the add() method, killing and removing from the TM list
        /// the oldest one(First-In, First-Out) when the max size is reached
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public bool Add(Process process)
        {
            if (_queue.Count == Size)
            {
                var proc = _queue.Dequeue();
                proc.Kill();
            }

            _queue.Enqueue(process);
            return true;
        }

        public Process Get(string pid) => _queue.ToList().SingleOrDefault(x => x.PID == pid);
        public IEnumerable<Process> GetAll() => _queue.ToList();
    }
}