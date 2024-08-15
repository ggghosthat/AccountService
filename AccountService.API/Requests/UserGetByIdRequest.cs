using AccountService.Entity;
using MediatR;

namespace AccountService.API.Requests;

public record class UserGetByIdRequest(Guid userId) : IRequest<User>;