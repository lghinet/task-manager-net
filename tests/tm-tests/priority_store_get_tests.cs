using System;
using System.Linq;
using FluentAssertions;
using task_manager;
using task_manager.ProcessStore;
using Xunit;

namespace tm_tests
{
    public class priority_store_get_tests
    {

        [Fact]
        public void get_by_pid()
        {
            var store = new PriorityProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));
            store.Add(new Process("3"));


            var p = store.Get("2");
            p.PID.Should().Be("2");
        }

        [Fact]
        public void get_by_pid_is_null()
        {
            var store = new PriorityProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));
            store.Add(new Process("3"));


            var p = store.Get("22");
            p.Should().BeNull();
        }

        [Fact]
        public void get_all()
        {
            var store = new PriorityProcessStore(3);
            store.Add(new Process("1", ProcessPriority.High));
            store.Add(new Process("2"));
            store.Add(new Process("3"));
            store.Add(new Process("4"));
            store.Add(new Process("5"));


            var list = store.GetAll();
            list.Select(x => x.PID)
                .Should()
                .BeEquivalentTo("1", "2", "3")
                .And
                .HaveCount(3);
        }
    }
}