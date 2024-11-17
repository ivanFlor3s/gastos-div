using AutoMapper;
using Divtos.Application.Groups.Commands.Create;
using Divtos.Domain.Entities;

namespace Divtos.Application.Common.Automapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGroupCommand, Group>();
        }
    }
}
