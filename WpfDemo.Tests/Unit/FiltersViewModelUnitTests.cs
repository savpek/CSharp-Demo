using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SqlScriptRunner.GUI;
using WpfDemo.Components;
using WpfDemo.ViewModel;
using WpfDemo.ViewModel.Filters;

namespace WpfDemo.Tests.Unit
{
    [TestFixture]
    public class FiltersViewModelUnitTests
    {
        private MessageAggregator _messenger;
        private FiltersViewModel _filtersViewModel;
        private List<IFilter> _filters;

        [SetUp]
        public void Init()
        {
            _filters = new List<IFilter> { new ColorFilterViewModel() };
            _messenger = new MessageAggregator();
            _filtersViewModel = new FiltersViewModel(_filters, _messenger);
        }

        [Test]
        public void ApplyCommandInvoked_SendMessageToApplyFilters()
        {
            using (new AsynchronousIsDisabledContext())
            {
                List<IFilter> filtersReceived = null;
                _messenger.Subscribe<Messages.FiltersAppliedMessage>(m => filtersReceived = m.Filters);

                _filtersViewModel.Apply.Execute(null);

                filtersReceived.Should().NotBeNull();
                filtersReceived.Should().ContainSingle(x => x.GetType() == typeof (ColorFilterViewModel));
            }
        }
    }
}
