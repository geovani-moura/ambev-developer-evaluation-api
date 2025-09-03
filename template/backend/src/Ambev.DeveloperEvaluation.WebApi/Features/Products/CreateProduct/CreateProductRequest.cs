namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequest
{
    public string Title { get; set; } = string.Empty;
    public required decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public required CreateProductRatingRequest Rating { get; set; }

}

public class CreateProductRatingRequest
{
    public required double Rate { get; set; }
    public required int Count { get; set; }
}
