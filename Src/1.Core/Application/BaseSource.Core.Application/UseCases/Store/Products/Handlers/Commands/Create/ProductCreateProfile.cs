using AutoMapper;
using BaseSource.Core.Domain.Aggregates.Store.Products.Parameters;

namespace BaseSource.Core.Application.UseCases.Store.Products.Handlers.Commands.Create;

public class ProductCreateProfile : Profile
{
    public ProductCreateProfile()
    {
        CreateMap<ProductCreateCommand, ProductCreateParameters>().ReverseMap();
    }
}