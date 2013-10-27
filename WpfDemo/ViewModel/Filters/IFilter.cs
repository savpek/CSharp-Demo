using System.Linq;
using WpfDemo.Domain;

namespace WpfDemo.ViewModel.Filters
{
    public interface IFilter
    {
        IQueryable<CarImage> Filter(IQueryable<CarImage> input);
    }
}
