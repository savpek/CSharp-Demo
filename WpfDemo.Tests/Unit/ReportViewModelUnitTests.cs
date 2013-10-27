using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SqlScriptRunner.GUI;
using WpfDemo.Domain;
using WpfDemo.Messages;
using WpfDemo.ViewModel;
using WpfDemo.ViewModel.Filters;

namespace WpfDemo.Tests.Unit
{   
    [TestFixture]
    public class ReportViewModelUnitTests
    {
        private InMemoryRepository _repository;
        private MessageAggregator _messagenger;
        private ReportViewModel _viewModel;

        [SetUp]
        public void Init()
        {
            _repository = new InMemoryRepository();
            _messagenger = new MessageAggregator();
            _viewModel = new ReportViewModel(_repository, _messagenger);
        }

        [Test]
        public void DontAcceptNulls_Constructor()
        {
            Action a = () => new ReportViewModel(null, _messagenger);
            a.ShouldThrow<ArgumentNullException>();

            a = () => new ReportViewModel(_repository, null);
            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Initially_ViewModelIsEmpty()
        {
            _viewModel.CarImages.Should().BeEmpty();
        }

        [Test]
        public void FiltersAppliedMessageReceived_UpdateData()
        {
            var carImage = new CarImage();
            _repository.Add(carImage);

            _messagenger.Publish(new FiltersAppliedMessage());

            _viewModel.CarImages.Should().Contain(carImage);
        }

        [Test]
        public void FiltersAppliedMultipleTimes_DontAddItemsMultipleTimes()
        {
            var carImage = new CarImage();
            _repository.Add(carImage);

            _messagenger.Publish(new FiltersAppliedMessage());
            _messagenger.Publish(new FiltersAppliedMessage());

            _viewModel.CarImages.Count.Should().Be(1);
            _viewModel.CarImages.Should().Contain(carImage);
        }

        private class RedFilter : IFilter
        {
            public IQueryable<CarImage> Filter(IQueryable<CarImage> input)
            {
                return input.Where(x => x.Color != "Red");
            }
        }

        private class BlackFilter : IFilter
        {
            public IQueryable<CarImage> Filter(IQueryable<CarImage> input)
            {
                return input.Where(x => x.Color != "Black");
            }
        }

        [Test]
        public void ApplyFilter_FilterDataCorrectly()
        {
            var carImage = new CarImage {Color = "Red"};
            var secondCarImage = new CarImage { Color = "Blue" };
            var thirdCarImage = new CarImage { Color = "Black" };
            _repository.Add(carImage);
            _repository.Add(secondCarImage);
            _repository.Add(thirdCarImage);

            _messagenger.Publish(new FiltersAppliedMessage(new BlackFilter(), new RedFilter()));

            _viewModel.CarImages.Count.Should().Be(1);
            _viewModel.CarImages.Should().Contain(secondCarImage);
        }
    }
}
