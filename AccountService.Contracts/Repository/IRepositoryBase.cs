using AccountService.Entity;
using AccountService.Contracts.Requests;
using System.Linq;
using System.Linq.Expressions;

namespace AccountService.Contracts.Repository;

public interface IRepository
{
    public Guid RepositoryId { get; }
}

public interface IRepositoryBase<T>
{
    public void Create(T item);
    public void Delete(T item);
}