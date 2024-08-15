using AccountService.Contracts.Repository;
using AccountService.Contracts.Requests;
using AccountService.Entity;
using AccountService.Repository.Base;
using AccountService.Repository.Context;
using AccountService.Repository.Extenssions;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private static Guid _repositoryId = Guid.NewGuid();
    private static string _repositoryName = "UserRepository";

    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {}

    public override Guid RepositoryId => _repositoryId;

    public static string RepositoryName => _repositoryName; 

    public void CreateUser(User user)
        => Create(user);

    public void DeleteUser(User user)
        => Delete(user);

    public async Task<User?> GetUserById(Guid id)
        => await FindByCondition(u => u.Id.Equals(id)).SingleOrDefaultAsync();

    public async Task<IEnumerable<User>> FilterUsers(UserParameters userParameters)
        => await GetAll().Filter(userParameters).ToListAsync();
}