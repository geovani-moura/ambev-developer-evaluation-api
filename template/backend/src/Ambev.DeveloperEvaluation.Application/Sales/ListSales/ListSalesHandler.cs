using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesHandler : IRequestHandler<ListSalesCommand, ListSalesResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    public ListSalesHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ListSalesResult> Handle(ListSalesCommand request, CancellationToken cancellationToken)
    {
        var sales = await _repository.ListAsync(
            request.Page,
            request.Size,
            request.Order,
            cancellationToken
        );

        var result = _mapper.Map<ListSalesResult>(sales);
        return result;
    }
}
