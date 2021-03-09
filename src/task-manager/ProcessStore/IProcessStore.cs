using System.Collections.Generic;

namespace task_manager.ProcessStore
{
    public interface IProcessStore
    {
        bool Add(Process process);
        Process Get(string pid);
        IEnumerable<Process> GetAll();
    }
}
