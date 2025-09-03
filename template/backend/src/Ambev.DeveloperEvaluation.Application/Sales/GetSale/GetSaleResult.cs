namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? SaleNumber { get; set; } = null;
    public DateTime Date { get; set; } = DateTime.UtcNow;

    // Dados do cliente (denormalizado)
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;

    public string Branch { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }

    public List<GetSaleItemResult> Items { get; set; } = [];
}
