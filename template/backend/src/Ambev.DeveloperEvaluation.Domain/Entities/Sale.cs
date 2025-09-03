namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
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
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    public List<SaleItem> Items { get; set; } = [];

    public void AddItem(Guid productId, string productTitle, string productCategory, int quantity, decimal unitPrice)
    {
        if (quantity > 20)
            throw new InvalidOperationException("Quantidade máxima de 20 unidades por produto.");

        var discount = 0m;
        if (quantity >= 4 && quantity < 10)
            discount = 0.10m;
        else if (quantity >= 10)
            discount = 0.20m;

        var item = new SaleItem
        {
            Id = Guid.NewGuid(),
            SaleId = Id,
            ProductId = productId,
            ProductTitle = productTitle,
            ProductCategory = productCategory,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Discount = discount,
            TotalAmount = quantity * unitPrice * (1 - discount)
        };

        Items.Add(item);
        RecalculateTotal();
    }

    public void Cancel() => IsCancelled = true;

    private void RecalculateTotal()
    {
        TotalAmount = Items.Sum(i => i.TotalAmount);
    }
}
