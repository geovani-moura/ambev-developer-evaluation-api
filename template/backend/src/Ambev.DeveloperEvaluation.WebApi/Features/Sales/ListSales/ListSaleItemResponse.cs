namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSaleItemResponse
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductCategory { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}
