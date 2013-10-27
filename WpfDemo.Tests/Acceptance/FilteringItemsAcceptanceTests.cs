using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SqlScriptRunner.GUI;
using WpfDemo.Components;
using WpfDemo.Domain;
using WpfDemo.ViewModel;
using WpfDemo.ViewModel.Filters;

namespace WpfDemo.Tests.Acceptance
{
    [TestFixture]
    public class FilteringItemsAcceptanceTests
    {
        private IRepository _repository;
        private MessageAggregator _messenger;
        private ReportViewModel _reportsViewModel;
        private ColorFilterViewModel _colorFilter;
        private RegisterPlateFilterViewModel _registerFilter;
        private FiltersViewModel _filtersViewModel;

        [SetUp]
        public void Init()
        {
            _repository = TestDataTemplates.CreateRepository();
            _messenger = new MessageAggregator();
            _reportsViewModel = new ReportViewModel(_repository, _messenger);

            _colorFilter = new ColorFilterViewModel();
            _registerFilter = new RegisterPlateFilterViewModel();
            
            var filters = new List<IFilter> {_colorFilter, _registerFilter};

            _filtersViewModel = new FiltersViewModel(filters, _messenger);
        }

        [Test]
        public void ApplyFilters_ChainShowsCorrectResults()
        {
            using (new AsynchronousIsDisabledContext())
            {
                // User change settings in ui.
                _colorFilter.ColorLike = "Red";
                _registerFilter.RegisterPlateLike = "30";

                // User presses apply button.
                _filtersViewModel.Apply.Execute(null);

                // Report view should be updated.
                _reportsViewModel.CarImages.Count.Should().Be(1);
                _reportsViewModel.CarImages[0].ShouldHave()
                    .SharedProperties().EqualTo(
                        new CarImage { Color = "Red", RegisterPlate = "IAC-300", Speed = 84 });
            }
        }
    }
}
