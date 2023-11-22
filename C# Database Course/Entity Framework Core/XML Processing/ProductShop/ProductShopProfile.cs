using AutoMapper;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //Profiles for Import:

            CreateMap<ImportUserDTO, User>();

            CreateMap<ImportProductDTO, Models.Product>();

            CreateMap<ImportCategoryDTO, Category>();

            CreateMap<ImportCategoryProductDTO, CategoryProduct>();

            //Profiles for Export:

        }
    }
}
