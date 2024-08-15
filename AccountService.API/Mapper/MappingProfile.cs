using AccountService.Entity;
using AutoMapper;

namespace AccountHandler.API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}