using System;
using System.Linq;
using FluentAssertions;
using task_manager;
using task_manager.ProcessStore;
using Xunit;

namespace tm_tests
{
    public class default_store_add_tests
    {

        [Fact]
        public void add_should_be_true()
        {
            var store = new DefaultProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));

            var result = store.Add(new Process("4"));
            result.Should().BeTrue();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "2", "4");

        }

        [Fact]
        public void add_should_be_false_max_capacity()
        {
            var store = new DefaultProcessStore(3);
            store.Add(new Process("1"));
            store.Add(new Process("2"));
            store.Add(new Process("3"));

            var result = store.Add(new Process("4"));
            result.Should().BeFalse();

            var list = store.GetAll();
            list.Select(x => x.PID).Should().BeEquivalentTo("1", "2", "3");

        }
        
    }
}