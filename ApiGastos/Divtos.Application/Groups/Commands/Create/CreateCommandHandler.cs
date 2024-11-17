using Divtos.Application.Common.Interfaces.Persistence;
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

        public CreateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ErrorOr<int>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = ObjectMap
            _unitOfWork.
        }
    }
}
