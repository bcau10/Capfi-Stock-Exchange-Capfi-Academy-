using core.Model;
using core.Repositories;
using core.Specifications;
using infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).ToListAsync();

    public void Add(T entity) => _context.Set<T>().Add(entity);

    public void Delete(T enity) => _context.Set<T>().Remove(enity);
    
    public async Task<int> CountAsync(ISpecification<T> spec) 
        => await ApplySpecification(spec).CountAsync();

    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        => await  ApplySpecification(spec).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<T>> GetListAllAsync() => await _context.Set<T>().ToListAsync();

    public void Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        => SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
}