namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesResult
{
    public List<ListSaleResult> Sales { get; set; } = [];
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}