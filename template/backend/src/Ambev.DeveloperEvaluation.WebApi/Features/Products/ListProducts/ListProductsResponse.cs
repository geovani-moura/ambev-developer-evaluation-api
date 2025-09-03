namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsResponse
{
    public List<ListProductResponse> Data { get; set; } = [];
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

}

public class ListProductResponse
{
    public System.Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ListProductRatingResponse Rating { get; set; } = new();
}

public class ListProductRatingResponse
{
    public double Rate { get; set; }
    public int Count { get; set; }
}