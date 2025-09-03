﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public CreateProductRatingResponse Rating { get; set; } = new();
}

public class CreateProductRatingResponse
{
    public double Rate { get; set; }
    public int Count { get; set; }
}