using AccountService.Contracts.Repository;
using AccountService.Contracts.Requests;
using AccountService.Entity;
using AccountService.Repository.Context;
using AccountService.Repository.Extenssions;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repository.Base;

public class RepositoryBase<T> : IRepository, IRepositoryBase<T> where T : class
{
    private RepositoryContext _repositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public virtual Guid RepositoryId { get; }

    public void Create(T item) =>
        _repositoryContext.Set<T>().Add(item);

    public void Delete(T item) =>
        _repositoryContext.Set<T>().Remove(item);

    public IQueryable<T> GetAll() =>
		    _repositoryContext.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        _repositoryContext.Set<T>().Where(expression).AsNoTracking();
}