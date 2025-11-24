using AutoMapper;
using PermissionSystem.Application.Data.Entities;
using PermissionSystem.Application.DTOs;

namespace PermissionSystem.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
        .ForMember(dest => dest.Groups,
            opt => opt.MapFrom(src => src.GroupUsers.Select(gu => gu.Group))).ReverseMap();

        CreateMap<UserCreateDTO, User>()
            .ForMember(dest => dest.GroupUsers, opt => opt.Ignore());

        CreateMap<SystemEntity, SystemDTO>().ReverseMap();
        CreateMap<Group, GroupDTO>().ReverseMap();
    }
}
