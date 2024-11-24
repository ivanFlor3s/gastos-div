using AutoMapper;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Entities;
using ErrorOr;
using MediatR;
using Divtos.Application.Common.Interfaces.Authentication;

namespace Divtos.Application.Groups.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateGroupCommand, ErrorOr<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly ICurrentUserProvider _currentUserProvider;
        

        public CreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserProvider currentUserProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<ErrorOr<int>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = _mapper.Map<Group>(request);
            var currentUser = _currentUserProvider.GetCurrentUser();
            group.GroupUsers = new List<GroupUser>()
            {
                new() { UserId = currentUser.Id, IsAdmin = true }
            };
            await _unitOfWork.Groups.AddAsync(group);
            await _unitOfWork.CompleteAsync();
            return group.Id;
        }
    }
}
