using ApiGastos.Dtos;
using ApiGastos.Dtos.Responses;
using ApiGastos.Entities;
using AutoMapper;

namespace ApiGastos.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserCreationDTO, AppUser>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<UserCredentialsDto, AppUser>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                ;
            CreateMap<GroupCreationDto, Group>();

            CreateMap<GroupUser, AppUserResponse>()
                .ForMember(opts => opts.Id, act => act.MapFrom(src => src.AppUser.Id))
                .ForMember(opts => opts.FirstName, act => act.MapFrom(src => src.AppUser.FirstName))
                .ForMember(opts => opts.LastName, act => act.MapFrom(src => src.AppUser.LastName))
                .ForMember(opts => opts.Email, act => act.MapFrom(src => src.AppUser.Email));

            CreateMap<Group, GroupResponse>()
                .AfterMap((src, dest, context) =>
                {
                    dest.Users = context.Mapper.Map<List<AppUserResponse>>(src.GroupUsers);
                });
            CreateMap<GroupResponse, Group>();
         

        }
        
    }
}
