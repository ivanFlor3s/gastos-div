using AutoMapper;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Application.Groups.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateGroupCommand, ErrorOr<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<int>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = _mapper.Map<Group>(request);
            await _unitOfWork.Groups.AddAsync(group);
            await _unitOfWork.CompleteAsync();
            return group.Id;
        }
    }
}
