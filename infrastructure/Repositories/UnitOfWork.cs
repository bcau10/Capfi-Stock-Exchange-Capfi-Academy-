using core.Model;
using core.Repositories;
using infrastructure.Data;
using System.Collections;

namespace infrastructure.Repositories;


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
    
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        _repositories ??= new Hashtable();

        var typeName = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(typeName))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType
                .MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(typeName, repositoryInstance);
        }

        return _repositories[typeName] as IGenericRepository<TEntity>;
    }
}