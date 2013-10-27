using System.Collections.Generic;
using System.Linq;

namespace WpfDemo.Domain
{
    public class InMemoryRepository : IRepository
    {
        private readonly IList<object> _items = new List<object>();

        public void Add<T>(T target)
        {
            _items.Add(target);
        }

        public void Delete<T>(T target)
        {
            _items.Remove(target);
        }

        public IQueryable<T> Query<T>()
        {
            return _items.OfType<T>().ToList().AsQueryable();
        }
    }
}
