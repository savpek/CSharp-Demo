using System.Linq;
using WpfDemo.Domain;

namespace WpfDemo.ViewModel.Filters
{
    public class ColorFilterViewModel : IFilter
    {
        public virtual string ColorLike { get; set; }

        public IQueryable<CarImage> Filter(IQueryable<CarImage> input)
        {
            if (string.IsNullOrEmpty(ColorLike))
                return input;

            return input.Where(x => x.Color == ColorLike);
        }
    }
}