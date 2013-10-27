using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using WpfDemo.Components;
using WpfDemo.ViewModel.Filters;

namespace WpfDemo.ViewModel
{
    public class FiltersViewModel
    {
        private readonly MessageAggregator _messenger;
        private List<IFilter> _filters;

        public FiltersViewModel(List<IFilter> filters, MessageAggregator messenger)
        {
            _filters = filters;
            _messenger = messenger;
        }

        private void ApplyFilters()
        {
            _messenger.Publish(new Messages.FiltersAppliedMessage { Filters = _filters});
        }

        private bool CommandAllwaysAvailable()
        {
            return true;
        }

        public ICommand Apply { get { return new AsyncCommand(ApplyFilters, CommandAllwaysAvailable); } }
    }
}
