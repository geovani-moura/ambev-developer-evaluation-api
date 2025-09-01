using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCategories;


public class GetCategoriesProfile : Profile
{
    public GetCategoriesProfile()
    {
        // como GetCategoriesResult só devolve lista de string, não precisa mapear nada extra
    }
}
