using AutoMapper;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Application.Groups.Commons;
using ErrorOr;
using MediatR;

namespace Divtos.Application.Groups.Queries.GetAll;

public class GetAllQueryHandler: IRequestHandler<GetAllQuery, ErrorOr<ICollection<GroupItemResult>>>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<ICollection<GroupItemResult>>> Handle(GetAllQuery query, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Groups.GetAllAsync();
        
        var groupItemResults = _mapper.Map<List<GroupItemResult>>(result);
        return groupItemResults;
    }
}