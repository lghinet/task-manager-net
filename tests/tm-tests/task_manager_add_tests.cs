using System;
using System.Linq;
using FluentAssertions;
using task_manager;
using task_manager.ProcessStore;
using Xunit;

namespace tm_tests
{
    public class task_manager_add_tests
    {

        [Fact]
        public void add_should_be_false_default_store()
        {
            var store = new TaskManager(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));
            store.Add(new Process("3", ProcessPriority.High));

            var result = store.Add(new Process("4"));
            result.Should().BeFalse();

            var list = store.List(SortBy.Priority);
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "3", "2");
        }

        [Fact]
        public void add_should_be_true_fifo_store()
        {
            var store = new TaskManager(3, TaskManagerStoreStrategy.FIFO);
            store.Add(new Process("1"));
            store.Add(new Process("2"));
            store.Add(new Process("3"));

            var result = store.Add(new Process("4"));
            result.Should().BeTrue();

            var list = store.List();
            list.Select(x => x.PID).Should().BeEquivalentTo("2", "3", "4");
        }


        [Fact]
        public void add_should_be_true_higher_priority()
        {
            var store = new TaskManager(3, TaskManagerStoreStrategy.Priority);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2", ProcessPriority.Medium));
            store.Add(new Process("3", ProcessPriority.Medium));

            var result = store.Add(new Process("4", ProcessPriority.High));
            result.Should().BeTrue();

            var list = store.List(SortBy.Priority);
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "4", "3");

        }

      
    }
}