using System.Collections.Generic;

namespace task_manager.ProcessStore
{
    public interface IProcessStore
    {
        bool Add(Process process);
        bool Remove(string pid);
        Process Get(string pid);
        IEnumerable<Process> GetAll();
    }
}
