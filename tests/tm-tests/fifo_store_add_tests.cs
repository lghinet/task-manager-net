using System;
using System.Linq;
using FluentAssertions;
using task_manager;
using task_manager.ProcessStore;
using Xunit;

namespace tm_tests
{
    public class fifo_store_add_tests
    {

        [Fact]
        public void add_should_be_true_same_priority()
        {
            var store = new FIFOProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));
            store.Add(new Process("3"));

            var result = store.Add(new Process("4"));
            result.Should().BeTrue();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("2", "3", "4");

        }

        [Fact]
        public void add_should_be_true_all_same_priority()
        {
            var store = new FIFOProcessStore(3);
            store.Add(new Process("1"));
            store.Add(new Process("2"));
            store.Add(new Process("3"));

            var result = store.Add(new Process("4"));
            result.Should().BeTrue();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("2", "3", "4");

        }


        [Fact]
        public void add_should_be_true_lower_priority()
        {
            var store = new FIFOProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2", ProcessPriority.Medium));
            store.Add(new Process("3", ProcessPriority.Medium));

            var result = store.Add(new Process("4", ProcessPriority.Low));
            result.Should().BeTrue();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("2", "3", "4");

        }

    }
}