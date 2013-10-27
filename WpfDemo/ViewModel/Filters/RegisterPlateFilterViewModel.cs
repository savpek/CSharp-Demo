using System.Linq;
using WpfDemo.Domain;

namespace WpfDemo.ViewModel.Filters
{
    public class RegisterPlateFilterViewModel : IFilter
    {
        public virtual string RegisterPlateLike { get; set; }

        public IQueryable<CarImage> Filter(IQueryable<CarImage> input)
        {
            if (string.IsNullOrEmpty(RegisterPlateLike))
                return input;

            return input.Where(x => x.RegisterPlate.Contains(RegisterPlateLike));
        }
    }
}