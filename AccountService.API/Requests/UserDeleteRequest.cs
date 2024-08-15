using AccountService.Entity;
using MediatR;

namespace AccountService.API.Requests;

public record class UserDeleteRequest(Guid userId) : IRequest;