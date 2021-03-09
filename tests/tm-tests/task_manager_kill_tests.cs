using System;
using System.Linq;
using FluentAssertions;
using task_manager;
using task_manager.ProcessStore;
using Xunit;

namespace tm_tests
{
    public class task_manager_kill_tests
    {

        [Fact]
        public void kill_by_pid()
        {
            var store = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2", ProcessPriority.Medium));
            store.Add(new Process("3", ProcessPriority.Medium));

            var p4 = new Process("4", ProcessPriority.High);
            store.Add(p4);
            store.Kill("4");
            p4.Running.Should().BeFalse();
        }

        [Fact]
        public void kill_by_group()
        {
            var store = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2", ProcessPriority.Medium));
            var p3 = new Process("3", ProcessPriority.Medium);
            store.Add(p3);
            store.Add(new Process("4", ProcessPriority.High));
            store.Kill(ProcessPriority.Medium);

            var list = store.List();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "4");

            p3.Running.Should().BeFalse();
        }

        [Fact]
        public void kill_all()
        {
            var store = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2", ProcessPriority.Medium));
            store.Add(new Process("4", ProcessPriority.High));
            
            store.KillAll();

            var list = store.List();
            list.Should().BeEmpty();
        }
    }
}