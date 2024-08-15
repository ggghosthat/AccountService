using AccountService.Contracts.Requests;
using AccountService.Entity;

namespace AccountService.Contracts.Repository;

public interface IUserRepository
{
    public void CreateUser(User user);
    public void DeleteUser(User user);

    public Task<User?> GetUserById(Guid id);
    public Task<IEnumerable<User>> FilterUsers(UserParameters userParameters);
}