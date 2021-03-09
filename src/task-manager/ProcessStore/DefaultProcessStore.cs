using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace task_manager.ProcessStore
{
    public class DefaultProcessStore : IProcessStore
    {
        private int Size { get; }
        private readonly List<Process> _processes = new List<Process>();

        public DefaultProcessStore(int capacity)
        {
            Size = capacity;
        }


        /// <summary>
        /// The default behaviour is that we can accept new processes till when there is capacity
        /// inside the Task Manager, otherwise we won’t accept any new process
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public bool Add(Process process)
        {
            if (_processes.Count >= Size) 
                return false;

            _processes.Add(process);
            return true;

        }

        public Process Get(string pid) => _processes.SingleOrDefault(x => x.PID == pid);
        public IEnumerable<Process> GetAll() => _processes;
    }
}