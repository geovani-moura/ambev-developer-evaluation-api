namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Request parameters for listing sales with pagination and ordering.
/// </summary>
public class ListSalesRequest
{
    /// <summary>
    /// Page number (default: 1).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page (default: 10).
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Ordering string. 
    /// Example: "salenumber asc, date desc".  
    /// Allowed fields: id, salenumber, date, customerid, customername, branch or totalamount.
    /// </summary>
    public string? Order { get; set; }
}
