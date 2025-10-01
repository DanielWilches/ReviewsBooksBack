using System.Linq.Expressions;

namespace Books.Domain.Layer.Interfaces
{
    public interface IRepository<T>
    {
        Task <T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T? entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
