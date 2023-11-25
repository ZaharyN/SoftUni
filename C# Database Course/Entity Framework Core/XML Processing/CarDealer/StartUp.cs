using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;
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

        }
        private static Mapper GetMapper()
        {
            MapperConfiguration configuration =
                new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

            return new Mapper(configuration);
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
                if(!context.Cars.Any(c => c.Id == importSale.CarId))
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

        public static string GetCarsWithDistance(CarDealerContext context)
        {


            return string.Empty;
        }
    }
}