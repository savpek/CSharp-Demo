using System.Collections.Generic;
using WpfDemo.ViewModel.Filters;

namespace WpfDemo.Messages
{
    public class FiltersAppliedMessage
    {
        public FiltersAppliedMessage()
        {
            Filters = new List<IFilter> {new PassEverythingFilter()};
        }

        public FiltersAppliedMessage(params IFilter[] filters)
        {
            Filters = new List<IFilter>(filters);
        }

        public List<IFilter> Filters { get; set; }
    }
}
