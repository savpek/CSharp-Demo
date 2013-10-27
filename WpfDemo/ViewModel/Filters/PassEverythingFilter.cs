using System.Linq;
using WpfDemo.Domain;

namespace WpfDemo.ViewModel.Filters
{
    public class PassEverythingFilter : IFilter
    {
        public IQueryable<CarImage> Filter(IQueryable<CarImage> input)
        {
            return input;
        }
    }
}