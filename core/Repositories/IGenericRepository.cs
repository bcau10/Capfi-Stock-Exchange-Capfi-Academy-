using core.Model;
using core.Specifications;

namespace core.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> GetListAllAsync();
    Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> specification);
    void Add(T entity);
    void Update(T entity);
    void Delete(T enity);
}