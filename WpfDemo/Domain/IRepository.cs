using System.Linq;

namespace WpfDemo.Domain
{
    public interface IRepository
    {
        void Add<T>(T target);
        void Delete<T>(T target);
        IQueryable<T> Query<T>();
    }
}