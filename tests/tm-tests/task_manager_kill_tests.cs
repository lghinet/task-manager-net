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
            var manager = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            manager.Add(new Process("1", ProcessPriority.High));
            manager.Add(new Process("2", ProcessPriority.Medium));
            manager.Add(new Process("3", ProcessPriority.Medium));

            var p4 = new Process("4", ProcessPriority.High);
            manager.Add(p4);
            manager.Kill("4");
            p4.Running.Should().BeFalse();
            manager.List().Count.Should().Be(2);
        }

        [Fact]
        public void kill_by_group()
        {
            var manager = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            manager.Add(new Process("1", ProcessPriority.High));
            manager.Add(new Process("2", ProcessPriority.Medium));
            var p3 = new Process("3", ProcessPriority.Medium);
            manager.Add(p3);
            manager.Add(new Process("4", ProcessPriority.High));
            manager.Kill(ProcessPriority.Medium);

            var list = manager.List();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "4");

            p3.Running.Should().BeFalse();
        }

        [Fact]
        public void kill_all()
        {
            var manager = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            manager.Add(new Process("1", ProcessPriority.High));
            manager.Add(new Process("2", ProcessPriority.Medium));
            manager.Add(new Process("4", ProcessPriority.High));

            manager.KillAll();

            var list = manager.List();
            list.Should().BeEmpty();
        }

        [Fact]
        public void kill_all_then_add_some_more()
        {
            var manager = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            manager.Add(new Process("1", ProcessPriority.High));
            manager.Add(new Process("2", ProcessPriority.Medium));
            manager.Add(new Process("4", ProcessPriority.High));

            manager.KillAll();

            var list = manager.List();
            list.Should().BeEmpty();

            manager.Add(new Process("2", ProcessPriority.Medium));
            manager.Add(new Process("3", ProcessPriority.Medium));
            list = manager.List();
            list.Count.Should().Be(2);
        }
    }
}