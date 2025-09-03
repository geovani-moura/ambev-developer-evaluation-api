using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public UpdateProductRatingRequest Rating { get; set; } = new();

}

public class UpdateProductRatingRequest
{
    public double Rate { get; set; }
    public int Count { get; set; }
}
