using Domain.Interfaces.Specification;

namespace Domain.Interfaces;

public interface IRepository<T> where T : IEntity
{
    Task<T?> FindByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> ListAsync();
    Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);

}