using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDTO, Supplier>();

            CreateMap<ImportPartDTO, Part>();

            CreateMap<ImportCustomerDTO, Customer>();

            CreateMap<ImportCarDTO, Car>();

            CreateMap<ImportSaleDTO, Sale>();

        }
        
    }
}
