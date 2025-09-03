namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleItemResponse
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }

    // Produto (denormalizado)
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductCategory { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}
