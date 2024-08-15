using AccountService.Entity;
using MediatR;

namespace AccountService.API.Requests;

public record class UserCreateRequest(UserDto userDto) : IRequest<User>;