using AutoMapper;
using Divtos.Application.Groups.Commands.Create;
using Divtos.Application.Groups.Commons;
using Divtos.Application.Spents.Commons;
using Divtos.Domain.Entities;

namespace Divtos.Application.Common.Automapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGroupCommand, Group>();
            CreateMap<Group, GroupDetailResult>();
            CreateMap<Group, GroupItemResult>()
                .ForMember(dest => dest.TotalSpent,
                    opt => opt.MapFrom(src => src.Spents.Sum(s => s.Amount)))
                .ForMember(dest => dest.Users,
                    opt => opt.MapFrom(src => src.GroupUsers.Select(gu => gu.User)));
                // .AfterMap((src, dest, context) =>
                // {
                //     var users =  src.GroupUsers.Select(gu => gu.User).ToList();
                //     dest.Users = context.Mapper.Map<List<GroupMember>>(users);
                // });
            CreateMap<User, GroupMember>();
            CreateMap<Spent, SpentResult>();
            
        }
    }
}
