namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    // Cliente denormalizado
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;

    public string Branch { get; set; } = string.Empty;

    // Totais
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }

    public List<CreateSaleItemResponse> Items { get; set; } = [];
}
