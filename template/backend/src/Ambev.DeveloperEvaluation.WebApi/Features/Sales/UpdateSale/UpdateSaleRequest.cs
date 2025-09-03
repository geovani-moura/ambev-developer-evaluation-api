using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    // Cliente (denormalizado)
    public required Guid CustomerId { get; set; }
    public required string CustomerName { get; set; }
    public required string CustomerEmail { get; set; }
    public required string CustomerPhone { get; set; }

    public string Branch { get; set; } = string.Empty;

    public required List<UpdateSaleItemRequest> Items { get; set; } = [];
}

public class UpdateSaleItemRequest
{
    public Guid Id { get; set; }
    public required Guid ProductId { get; set; }
    public required string ProductTitle { get; set; }
    public required string ProductCategory { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
}
