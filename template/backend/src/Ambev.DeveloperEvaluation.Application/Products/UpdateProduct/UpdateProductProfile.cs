using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        // ===== Domain -> Application =====
        CreateMap<Product, UpdateProductResult>();

        // ===== Application -> Domain =====
        CreateMap<UpdateProductCommand, Product>();
    }
}
