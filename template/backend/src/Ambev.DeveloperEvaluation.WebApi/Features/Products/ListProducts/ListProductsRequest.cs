namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsRequest
{
    public int Page { get; set; } = 1;   // _page
    public int Size { get; set; } = 10;  // _size
    public string? Order { get; set; }   // _order, ex: "price desc, title asc"
}
