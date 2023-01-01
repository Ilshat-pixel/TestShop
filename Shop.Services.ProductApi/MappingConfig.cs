using AutoMapper;
using Shop.Services.ProductApi.Models;
using Shop.Services.ProductApi.Models.Dto;

namespace Shop.Services.ProductApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });

            return mapperConfig;
        }
    }
}
