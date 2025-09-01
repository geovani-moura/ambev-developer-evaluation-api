using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetByCategory;
using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Common
{
    /// <summary>
    /// AutoMapper profile for Products feature:
    /// - Requests -> Commands
    /// - Results  -> Responses
    /// </summary>
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {
            // ===== Requests -> Commands =====
            CreateMap<Guid, GetProductCommand>()
                .ConstructUsing(id => new GetProductCommand(id));
            CreateMap<UpdateProductRequest, UpdateProductCommand>()
                .ForMember(d => d.RatingRate, o => o.MapFrom(s => s.Rating.Rate))
                .ForMember(d => d.RatingCount, o => o.MapFrom(s => s.Rating.Count));
            CreateMap<Guid, DeleteProductCommand>()
                .ConstructUsing(id => new DeleteProductCommand(id));
            CreateMap<ListProductsRequest, ListProductsCommand>();
            CreateMap<GetByCategoryRequest, GetByCategoryCommand>();

            // ===== Results -> Responses =====           
            CreateMap<ProductResult, ProductResponse>();
            CreateMap<CreateProductResult, CreateProductResponse>();
            CreateMap<GetProductResult, GetProductResponse>();
            CreateMap<UpdateProductResult, UpdateProductResponse>();
            CreateMap<DeleteProductResult, DeleteProductResponse>();
            CreateMap<ListProductsResult, ListProductsResponse>();
            CreateMap<GetCategoriesResult, GetCategoriesResponse>();
            CreateMap<GetByCategoryResult, GetByCategoryResponse>();
        }
    }
}
