using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetByCategory;
using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller responsável pelas operações de produtos.
/// </summary>
/// <remarks>
/// Este controller expõe endpoints REST para criar, consultar, atualizar, listar e excluir produtos.
/// 
/// Recursos disponíveis:
/// - Criar produtos
/// - Consultar por ID
/// - Atualizar por ID
/// - Excluir por ID
/// - Listar com paginação, ordenação e filtros por categoria
/// - Listar categorias existentes
/// 
/// A ordenação pode ser feita através do parâmetro `_order`.
/// Exemplos de uso:
/// - `_order=price desc` → Ordena por preço decrescente
/// - `_order=title asc, price desc` → Ordena primeiro por título crescente e depois por preço decrescente
/// 
/// Campos permitidos na ordenação:
/// - `id`
/// - `title`
/// - `price`
/// - `category`
/// </remarks>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Cria um novo produto.
    /// </summary>
    /// <param name="request">Dados do produto a ser criado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Produto criado com sucesso.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(result)
        });
    }

    /// <summary>
    /// Recupera um produto pelo seu identificador.
    /// </summary>
    /// <param name="id">ID único do produto.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Produto encontrado, ou erro caso não exista.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetProductCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetProductResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = _mapper.Map<GetProductResponse>(result)
        });
    }

    /// <summary>
    /// Remove um produto pelo seu identificador.
    /// </summary>
    /// <param name="id">ID único do produto.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Status da operação de exclusão.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        var deleted = await _mediator.Send(command, cancellationToken);

        if (!deleted.Success)
            return NotFound(new ApiResponse { Success = false, Message = "Product not found" });

        return Ok(new ApiResponse { Success = true, Message = "Product deleted successfully" });
    }

    /// <summary>
    /// Atualiza um produto existente.
    /// </summary>
    /// <param name="id">ID único do produto a ser atualizado.</param>
    /// <param name="request">Dados do produto a atualizar.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Produto atualizado, ou erro caso não seja encontrado.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateProductCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        if (result is null)
            return NotFound(new ApiResponse { Success = false, Message = "Product not found" });

        return Ok(new ApiResponseWithData<UpdateProductResponse>
        {
            Success = true,
            Message = "Product updated successfully",
            Data = _mapper.Map<UpdateProductResponse>(result)
        });
    }

    /// <summary>
    /// Lista os produtos com suporte a paginação e ordenação.
    /// </summary>
    /// <param name="_page">Número da página (padrão: 1).</param>
    /// <param name="_size">Quantidade de itens por página (padrão: 10).</param>
    /// <param name="_order">
    /// Campos de ordenação, podendo incluir direção asc/desc.  
    /// Exemplos:  
    /// - "price desc"  
    /// - "title asc, price desc"  
    /// 
    /// Campos permitidos: id, title, price, category.
    /// </param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Lista paginada de produtos.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<ListProductsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListProducts([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string? _order = null, CancellationToken cancellationToken = default)
    {
        var request = new ListProductsRequest { Page = _page, Size = _size, Order = _order };
        var validator = new ListProductsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<ListProductsCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<ListProductsResponse>(result));
    }

    /// <summary>
    /// Lista todas as categorias de produtos.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Lista de categorias.</returns>
    [HttpGet("categories")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCategoriesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var command = new GetCategoriesCommand();
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetCategoriesResponse>
        {
            Success = true,
            Message = "Categories retrieved successfully",
            Data = _mapper.Map<GetCategoriesResponse>(result)
        });
    }

    /// <summary>
    /// Lista produtos por categoria, com suporte a paginação e ordenação.
    /// </summary>
    /// <param name="category">Nome da categoria a filtrar.</param>
    /// <param name="_page">Número da página (padrão: 1).</param>
    /// <param name="_size">Quantidade de itens por página (padrão: 10).</param>
    /// <param name="_order">
    /// Campos de ordenação, podendo incluir direção asc/desc.  
    /// Exemplos:  
    /// - "price desc"  
    /// - "title asc, price desc"  
    /// 
    /// Campos permitidos: id, title, price, category.
    /// </param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Lista paginada de produtos filtrados pela categoria.</returns>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetByCategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByCategory(
        [FromRoute] string category,
        [FromQuery] int _page = 1,
        [FromQuery] int _size = 10,
        [FromQuery] string? _order = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetByCategoryRequest { Category = category, Page = _page, Size = _size, Order = _order };
        var validator = new GetByCategoryRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetByCategoryCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetByCategoryResponse>
        {
            Success = true,
            Message = "Products by category retrieved successfully",
            Data = _mapper.Map<GetByCategoryResponse>(result)
        });
    }
}
