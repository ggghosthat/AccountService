using AccountService.API.Requests;
using AccountService.Contracts.Repository;
using AccountService.Entity;
using AccountService.Repository;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;

namespace AccountService.API.Handlers;

public class AccountHandler 
    : IRequestHandler<UserCreateRequest, User>,
      IRequestHandler<UserDeleteRequest>,
      IRequestHandler<UserGetByIdRequest, User>,
      IRequestHandler<UserSearchRequest, IEnumerable<User>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public AccountHandler(
        IMapper mapper,
        IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<User> Handle(UserCreateRequest userCreateRequest, CancellationToken token)
    {                
        var user = _mapper.Map<User>(userCreateRequest.userDto);
        _repositoryManager.UserRepository.CreateUser(user);
        await _repositoryManager.SaveAsync();
        return user;
    }

    public async Task Handle(UserDeleteRequest userDeleteRequest, CancellationToken token)
    {
        var user = await _repositoryManager.UserRepository.GetUserById(userDeleteRequest.userId);
        
        if (user != null)
        {
            _repositoryManager.UserRepository.DeleteUser(user);
            await _repositoryManager.SaveAsync();
        }
    } 

    public async Task<User?> Handle(UserGetByIdRequest userGetByIdRequest, CancellationToken token)
    {
        return await _repositoryManager.UserRepository.GetUserById(userGetByIdRequest.userId);
    }

    public async Task<IEnumerable<User>> Handle(UserSearchRequest userSearchRequest, CancellationToken token)
    {
        return await _repositoryManager.UserRepository.FilterUsers(userSearchRequest.userParameters);
    }
}