namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsResult
{
    public List<ListProductResult> Data { get; set; } = [];
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

}

public class ListProductResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public ListProductRatingResult Rating { get; set; } = new();
}

public class ListProductRatingResult
{
    public double Rate { get; set; }
    public int Count { get; set; }
}
