using AutoMapper;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Application.Groups.Commons;
using Divtos.Domain.Commons.Errors;
using Divtos.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Divtos.Application.Groups.Queries.GetDetail;

public class GetDetailQueryHandler : IRequestHandler<GetDetailQuery, ErrorOr<GroupDetailResult>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GroupDetailResult>> Handle(GetDetailQuery query, CancellationToken cancellationToken)
    {
        var result = await  _unitOfWork.Groups.GetDetailAsync(query.IdGroup);
        if (result is not Group group)
        {
            return Errors.Group.NotFound(query.IdGroup) ;
        }

        var groupDetailResult = _mapper.Map<GroupDetailResult>(group);
        return groupDetailResult;
    }
}