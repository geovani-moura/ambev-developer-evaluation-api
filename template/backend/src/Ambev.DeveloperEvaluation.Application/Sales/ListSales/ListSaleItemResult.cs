namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSaleItemResult
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductCategory { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}


