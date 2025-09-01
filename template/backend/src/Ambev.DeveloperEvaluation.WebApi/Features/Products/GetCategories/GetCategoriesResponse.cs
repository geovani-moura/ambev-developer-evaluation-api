namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;

public class GetCategoriesResponse
{
    public IEnumerable<string> Categories { get; set; } = new List<string>();
}
