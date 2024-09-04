using ApiGastos.Dtos;
using ApiGastos.Dtos.Responses;
using ApiGastos.Dtos.Spent;
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
                    dest.Users = context.Mapper.Map<List<GroupMemberResponse>>(src.GroupUsers);
                });
            CreateMap<GroupResponse, Group>();
            CreateMap<AddSpentDto, Spent>()
                .ForMember(opts => opts.Participants, act => act.Ignore());
            CreateMap<AppUser, AppUserResponse>();

            CreateMap<Spent, SpentResponse>()
                .AfterMap((src, dest, context) =>
                {
                    dest.Participants = context.Mapper.Map<List<AppUserResponse>>(src.Participants);
                });

            CreateMap<SpentParticipant, AppUserResponse>()
                .ForMember(opts => opts.LastName, act => act.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName));

            CreateMap<Group, GroupBasicResponse>()
                .AfterMap((src, dest, context) =>
                {
                    dest.Users = context.Mapper.Map<List<AppUserResponse>>(src.GroupUsers);
                });

            CreateMap<GroupUser, GroupMemberResponse>()
               .ForMember(opts => opts.Id, act => act.MapFrom(src => src.AppUser.Id))
               .ForMember(opts => opts.FirstName, act => act.MapFrom(src => src.AppUser.FirstName))
               .ForMember(opts => opts.LastName, act => act.MapFrom(src => src.AppUser.LastName))
               .ForMember(opts => opts.IsAdmin, act => act.MapFrom(src => src.IsAdmin))
               .ForMember(opts => opts.Email, act => act.MapFrom(src => src.AppUser.Email));

            CreateMap<GoogleUserCreationDto, AppUser>()
                  .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email));
        }
        
    }
}
