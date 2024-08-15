using AccountService.Contracts.Repository;
using AccountService.Repository.Context;
using System.Collections.Concurrent;
using System.Reflection;

namespace AccountService.Repository;

public class RepositoryManager : IRepositoryManager
{
    private IUserRepository _userRepository;
    private readonly RepositoryContext _repositoryContext;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public IUserRepository UserRepository
    {
        get 
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_repositoryContext);

            return _userRepository;
        }
    } 

    public Task SaveAsync()
    {
        return _repositoryContext.SaveChangesAsync();
    }
}