using System.Threading.Tasks;

namespace AccountService.Contracts.Repository;

public interface IRepositoryManager
{
    public IUserRepository UserRepository { get; }

    public Task SaveAsync();
}