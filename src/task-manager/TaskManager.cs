using System;
using System.Collections.Generic;
using System.Linq;
using task_manager.ProcessStore;

namespace task_manager
{
    /// <summary>
    /// Task Manager is a software component that is designed for handling
    /// multiple processes inside an operating system
    /// </summary>
    public class TaskManager
    {
        private readonly IProcessStore _store;

        public TaskManager(int capacity, TaskManagerStoreStrategy storeStrategy = TaskManagerStoreStrategy.Default)
        {
            _store = CreateStore(capacity, storeStrategy);
        }

        private IProcessStore CreateStore(int capacity, TaskManagerStoreStrategy storeStrategy)
        {
            switch (storeStrategy)
            {
                case TaskManagerStoreStrategy.Priority: return new PriorityProcessStore(capacity);
                case TaskManagerStoreStrategy.FIFO: return new FIFOProcessStore(capacity);
                default: return new DefaultProcessStore(capacity);
            }
        }


        /// <summary>
        /// The task manager should have a prefixed maximum capacity, so it can not have more than a certain
        /// number of running processes within itself.This value is defined at build time.
        /// The add(process) method in TM is used for it.
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public bool Add(Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            return _store.Add(process);
        }

        /// <summary>
        /// The task manager offers the possibility to list() all the running processes,
        /// sorting them by time of creation (implicitly we can consider it the time in which
        /// has been added to the TM), priority or id. 
        /// </summary>
        public List<Process> List(SortBy sortBy = SortBy.CreationDate)
        {
            var query = _store
                .GetAll()
                .Where(x => x.Running);

            switch (sortBy)
            {
                case SortBy.PID: return query.OrderBy(x => x.PID).ToList();
                case SortBy.Priority: return query.OrderBy(x => x.Priority).ToList();
                default: return query.OrderBy(x => x.CreatedAt).ToList();
            }
        }

        /// <summary>
        ///  killing a specific process
        /// </summary>
        /// <param name="pid"></param>
        public void Kill(string pid)
        {
            var process = _store.Get(pid);
            process?.Kill();
        }

        /// <summary>
        /// killing all processes with a specific priority
        /// </summary>
        /// <param name="priority"></param>
        public void Kill(ProcessPriority priority)
        {
            var processes = _store
                .GetAll()
                .Where(x => x.Priority == priority && x.Running);

            foreach (var proc in processes)
            {
                proc.Kill();
                _store.Remove(proc.PID);
            }
        }

        /// <summary>
        /// killing all running processes
        /// </summary>
        public void KillAll()
        {
            foreach (var proc in _store.GetAll().Where(x => x.Running))
            {
                proc.Kill();
                _store.Remove(proc.PID);
            }
        }

    }

    public enum TaskManagerStoreStrategy
    {
        Default,
        FIFO,
        Priority
    }

    public enum SortBy
    {
        CreationDate,
        PID,
        Priority
    }
}