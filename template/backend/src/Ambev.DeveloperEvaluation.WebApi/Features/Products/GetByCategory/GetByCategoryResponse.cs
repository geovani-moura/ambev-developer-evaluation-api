namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory;

public class GetByCategoryResponse
{
    public IEnumerable<GetByCategoryProductResponse> Data { get; set; } = [];
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

}

public class GetByCategoryProductResponse
{
    public System.Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public GetByCategoryRatingResponse Rating { get; set; } = new();
}

public class GetByCategoryRatingResponse
{
    public double Rate { get; set; }
    public int Count { get; set; }
}