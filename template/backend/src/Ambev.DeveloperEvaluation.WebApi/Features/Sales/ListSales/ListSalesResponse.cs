namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesResponse
{
    public List<ListSaleResponse> Sales { get; set; } = new();
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
