using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WpfDemo.Domain;

namespace WpfDemo.Tests.Unit
{
    [TestFixture]
    public class InMemoryRepositoryUnitTests
    {
        private InMemoryRepository _repository;

        [SetUp]
        public void Init()
        {
            _repository = new InMemoryRepository();
        }

        [Test]
        public void WithMultipleItemsOfGivenType_ReturnItems()
        {
            var item = new object();
            var item2 = new object();
            _repository.Add(item);
            _repository.Add(item2);

            var results = _repository.Query<object>().ToList();

            results.Count().Should().Be(2);
            results.Should().Contain(item);
            results.Should().Contain(item2);
        }

        [Test]
        public void RemoveItem_ReturnItemsMinusRemoved()
        {
            var item = new object();
            var item2 = new object();
            _repository.Add(item);
            _repository.Add(item2);

            _repository.Delete(item);

            var results = _repository.Query<object>().ToList();
            results.Count().Should().Be(1);
            results.Should().Contain(item2);
        }

        private class DummyTypeA {}
        private class DummyTypeB { }
        
        [Test]
        public void QueryWithMultipleTypes_ReturnOnlyGivenType()
        {
            var item = new DummyTypeA();
            var item2 = new DummyTypeB();
            _repository.Add(item);
            _repository.Add(item2);

            var results = _repository.Query<DummyTypeA>().ToList();
            
            results.Count().Should().Be(1);
            results.Should().Contain(item);
        }
    }
}
