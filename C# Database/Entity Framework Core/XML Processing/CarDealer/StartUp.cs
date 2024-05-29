using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            //09.
            //string suppliersXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(context, suppliersXml));
            //10.
            //string xmlInput = File.ReadAllText("../../../Datasets/parts.xml");
            //Console.WriteLine(ImportParts(context, xmlInput));
            //11.
            //string xmlInput = File.ReadAllText("../../../Datasets/cars.xml");
            //Console.WriteLine(ImportCars(context, xmlInput));
            //12.
            //string xmlInput = File.ReadAllText("../../../Datasets/customers.xml");
            //Console.WriteLine(ImportCustomers(context, xmlInput));
            //13.
            //string xmlInput = File.ReadAllText("../../../Datasets/sales.xml");
            //Console.WriteLine(ImportSales(context, xmlInput));
            //14.
            //Console.WriteLine(GetCarsWithDistance(context));
            //15.
            //Console.WriteLine(GetCarsFromMakeBmw(context));
            //16.
            //Console.WriteLine(GetLocalSuppliers(context));
            //17.
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));
            //18.
            //Console.WriteLine(GetTotalSalesByCustomer(context));
            //19.
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }
        private static Mapper GetMapper()
        {
            MapperConfiguration configuration =
                new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

            return new Mapper(configuration);
        }
        private static string XmlSerializer<T>(T dto, string xmlRootAttributeName)
        {
            XmlSerializer serialaizer =
                new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));

            XmlSerializerNamespaces xmlns = new();
            xmlns.Add(string.Empty, string.Empty);

            StringBuilder sb = new();

            using (StringWriter sw = new(sb, CultureInfo.InvariantCulture))
            {
                try
                {
                    serialaizer.Serialize(sw, dto, xmlns);
                }
                catch (Exception)
                {
                    throw;
                }

            }

            return sb.ToString();
        }

        //09.
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportSupplierDTO[]),
                new XmlRootAttribute("Suppliers"));

            StringReader input = new StringReader(inputXml);

            ImportSupplierDTO[] suppliersDTO = serializer.Deserialize(input) as ImportSupplierDTO[];

            Mapper mapepr = GetMapper();

            Supplier[] suplliers = mapepr.Map<Supplier[]>(suppliersDTO);
            context.AddRange(suplliers);
            context.SaveChanges();

            return $"Successfully imported {suplliers.Length}";
        }

        //10.
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportPartDTO[]),
                new XmlRootAttribute("Parts"));

            StringReader input = new StringReader(inputXml);

            ImportPartDTO[] partsDTO = serializer.Deserialize(input) as ImportPartDTO[];

            Mapper mapper = GetMapper();

            Part[] parts = mapper.Map<Part[]>(partsDTO);

            Part[] validParts = parts
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .ToArray();

            context.Parts.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Length}";
        }

        //11.
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCarDTO[]),
                new XmlRootAttribute("Cars"));

            StringReader input = new StringReader(inputXml);

            ImportCarDTO[] carsDTO = serializer.Deserialize(input) as ImportCarDTO[];

            Mapper mapper = GetMapper();
            List<Car> cars = new List<Car>();

            foreach (var carDTO in carsDTO)
            {
                Car car = mapper.Map<Car>(carDTO);

                int[] validPartsIds = carDTO.PartsIds
                    .Select(p => p.Id)
                    .Distinct()
                    .ToArray();

                List<PartCar> partCars = new List<PartCar>();

                foreach (var validId in validPartsIds)
                {
                    partCars.Add(new PartCar
                    {
                        Car = car,
                        PartId = validId
                    });
                }

                car.PartsCars = partCars;
                cars.Add(car);
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        //12.
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(ImportCustomerDTO[]), new XmlRootAttribute("Customers"));

            StringReader reader = new StringReader(inputXml);

            ImportCustomerDTO[] customerDTO = serializer.Deserialize(reader) as ImportCustomerDTO[];

            Mapper mapper = GetMapper();
            Customer[] customers = mapper.Map<Customer[]>(customerDTO);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";

        }

        //13.
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(ImportSaleDTO[]), new XmlRootAttribute("Sales"));

            StringReader reader = new StringReader(inputXml);

            ImportSaleDTO[] importSales = serializer.Deserialize(reader) as ImportSaleDTO[];

            Mapper mapper = GetMapper();
            List<Sale> sales = new List<Sale>();

            foreach (var importSale in importSales)
            {
                if (!context.Cars.Any(c => c.Id == importSale.CarId))
                {
                    continue;
                }
                Sale currentSale = mapper.Map<Sale>(importSale);
                sales.Add(currentSale);
            }

            context.AddRange(sales);
            context.SaveChanges();


            return $"Successfully imported {sales.Count}";
        }

        //14.
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            string rootAttribute = "cars";

            ExportCarWithDistanceDTO[] carsWithDistance = context.Cars
                .Where(c => c.TraveledDistance > 2_000_000)
                .Select(c => new ExportCarWithDistanceDTO
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                .Take(10)
                .ToArray();

            return XmlSerializer<ExportCarWithDistanceDTO[]>(carsWithDistance, rootAttribute);
        }

        //15.
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            string rootAttribute = "cars";

            ExportCarsFromMakeBMWDTO[] carsFromBMW = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new ExportCarsFromMakeBMWDTO
                {
                    Id = c.Id,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TraveledDistance)
                .ToArray();


            return XmlSerializer<ExportCarsFromMakeBMWDTO[]>(carsFromBMW, rootAttribute);
        }

        //16.
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            ExportLocalSuppliersDTO[] localSuppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new ExportLocalSuppliersDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Parts = s.Parts.Count
                })
                .ToArray();

            string rootAttribute = "suppliers";

            return XmlSerializer<ExportLocalSuppliersDTO[]>(localSuppliers, rootAttribute);
        }

        //17.
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            ExportCarWithPartsDTO[] carsWithParts = context.Cars
                .Select(c => new ExportCarWithPartsDTO
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                    Parts = c.PartsCars.Select(pc => new ExportPartDTO
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price
                    })
                    .OrderByDescending(pc => pc.Price)
                    .ToArray()
                })
                .OrderByDescending(c => c.TraveledDistance)
                    .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            string rootAttribute = "cars";

            return XmlSerializer<ExportCarWithPartsDTO[]>(carsWithParts, rootAttribute);
        }

        //18.
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            string rootAttribute = "customers";

            var temp = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SalesInfo = c.Sales.Select(s => new
                    {
                        Prices = c.IsYoungDriver 
                                    ? s.Car.PartsCars.Sum(pc => Math.Round((double)pc.Part.Price * 0.95, 2))
                                    : s.Car.PartsCars.Sum(pc => (double)(pc.Part.Price))
                    })
                    .ToArray()
                })
                .ToArray();

            ExportTotalSalesByCustomerDTO[] customersWithSales = temp
                .Select(x => new ExportTotalSalesByCustomerDTO
                {
                    FullName = x.FullName,
                    BoughtCars = x.BoughtCars,
                    MoneySpent = x.SalesInfo.Sum(x => (decimal)x.Prices)
                })
                .OrderByDescending(x => x.MoneySpent)
                .ToArray();

            return XmlSerializer<ExportTotalSalesByCustomerDTO[]>(customersWithSales, rootAttribute);
        }

        //19.
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            ExportSaleWithDiscountDTO[] sales = context.Sales
                .Select(s => new ExportSaleWithDiscountDTO()
                {
                    Car = new ExportCarDTO()
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance
                    },
                    Discount = (int)s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartsCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = Math.Round((double)(s.Car.PartsCars.Sum(p => p.Part.Price) * (1 - (s.Discount / 100))), 4)
                })
                .ToArray();

           

            string rootAttribute = "sales";

            return XmlSerializer<ExportSaleWithDiscountDTO[]>(sales, rootAttribute);
        }
    }
}