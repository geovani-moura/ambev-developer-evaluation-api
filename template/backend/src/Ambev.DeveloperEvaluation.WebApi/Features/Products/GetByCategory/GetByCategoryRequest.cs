namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory;

public class GetByCategoryRequest
{
    public string Category { get; set; } = string.Empty;
    public int Page { get; set; } = 1;   // _page
    public int Size { get; set; } = 10;  // _size
    public string? Order { get; set; }   // _order
}