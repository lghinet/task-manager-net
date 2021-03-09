using System;
using System.Linq;
using FluentAssertions;
using task_manager;
using task_manager.ProcessStore;
using Xunit;

namespace tm_tests
{
    public class priority_store_add_tests
    {

        [Fact]
        public void add_should_be_false_same_priority()
        {
            var store = new PriorityProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));
            store.Add(new Process("3"));

            var result = store.Add(new Process("4"));
            result.Should().BeFalse();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "2", "3");

        }

        [Fact]
        public void add_should_be_false_all_same_priority()
        {
            var store = new PriorityProcessStore(3);
            store.Add(new Process("1"));
            store.Add(new Process("2"));
            store.Add(new Process("3"));

            var result = store.Add(new Process("4"));
            result.Should().BeFalse();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "2", "3");

        }


        [Fact]
        public void add_should_be_false_lower_priority()
        {
            var store = new PriorityProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2", ProcessPriority.Medium));
            store.Add(new Process("3", ProcessPriority.Medium));

            var result = store.Add(new Process("4", ProcessPriority.Low));
            result.Should().BeFalse();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "2", "3");

        }

        [Fact]
        public void add_should_be_true_higher_priority()
        {
            var store = new PriorityProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));
            store.Add(new Process("3"));

            var result = store.Add(new Process("4", ProcessPriority.Medium));
            result.Should().BeTrue();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "3", "4");

        }
    }
}