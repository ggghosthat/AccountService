using AccountService.Entity;
using AccountService.Contracts.Requests;
using MediatR;

namespace AccountService.API.Requests;

public record class UserSearchRequest(UserParameters userParameters) : IRequest<IEnumerable<User>>;