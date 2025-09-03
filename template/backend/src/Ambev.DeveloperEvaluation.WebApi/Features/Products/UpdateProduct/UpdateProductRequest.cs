using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public required decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public required UpdateProductRatingRequest Rating { get; set; }

}

public class UpdateProductRatingRequest
{
    public required double Rate { get; set; }
    public required int Count { get; set; }
}
