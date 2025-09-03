using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Cria uma nova venda.
    /// </summary>
    /// <param name="request">Dados da venda a ser criada.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Venda criada com sucesso.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(result)
        });
    }
    /// <summary>
    /// Lista todas as vendas com paginação, ordenação e filtros.
    /// </summary>
    /// <param name="page">Número da página (default = 1)</param>
    /// <param name="size">Itens por página (default = 10)</param>
    /// <param name="order">Ordenação, ex: "date desc, customerName asc"</param>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<ListSalesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSales(
        [FromQuery(Name = "_page")] int _page = 1,
        [FromQuery(Name = "_size")] int _size = 10,
        [FromQuery(Name = "_order")] string? _order = null,
        CancellationToken cancellationToken = default)
    {
        var request = new ListSalesRequest { Page = _page, Size = _size, Order = _order };
        var validator = new ListSalesRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<ListSalesCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<ListSalesResponse>(result));
    }

    /// <summary>
    /// Remove um sales pelo seu identificador.
    /// </summary>
    /// <param name="id">ID único do sales.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Status da operação de exclusão.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { Id = id };
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request.Id);
        var deleted = await _mediator.Send(command, cancellationToken);

        if (!deleted.Success)
            return NotFound(new ApiResponse { Success = false, Message = "Product not found" });

        return Ok(new ApiResponse { Success = true, Message = "Product deleted successfully" });
    }
}
