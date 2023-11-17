using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            //01.
            //string input = File.ReadAllText("../../../Datasets/suppliers.json");
            //Console.WriteLine(ImportSuppliers(context, input));

            //02.
            //string input = File.ReadAllText("../../../Datasets/parts.json");
            //Console.WriteLine(ImportParts(context, input));

            //03.
            //string input = File.ReadAllText("../../../Datasets/cars.json");
            //Console.WriteLine(ImportCars(context, input));

            //04.
            //string input = File.ReadAllText("../../../Datasets/customers.json"); 
            //Console.WriteLine(ImportCustomers(context, input));

            //05.
            //string input = File.ReadAllText("../../../Datasets/sales.json"); 
            //Console.WriteLine(ImportSales(context, input));

            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        //09.
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(configuration);

            SupplierDTO[] suppliersDTO = JsonConvert.DeserializeObject<SupplierDTO[]>(inputJson);

            Supplier[] suppliers = mapper.Map<Supplier[]>(suppliersDTO);


            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        //10.
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(configuration);

            PartDTO[] partsDTO = JsonConvert.DeserializeObject<PartDTO[]>(inputJson);
            Part[] parts = mapper.Map<Part[]>(partsDTO);

            Part[] validParts = parts
                .Where(p => context.Suppliers
                .Any(s => s.Id == p.SupplierId))
                .ToArray();

            context.Parts.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Length}.";
        }

        //11.
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(configuration);

            CarDTO[] carsDTO = JsonConvert.DeserializeObject<CarDTO[]>(inputJson);
            ICollection<Car> cars = new HashSet<Car>();

            foreach (var carDTO in carsDTO)
            {
                Car currentCar = mapper.Map<Car>(carDTO);

                foreach (var partID in carDTO.PartsId)
                {
                    if (context.Parts.Any(p => p.Id == partID))
                    {
                        currentCar.PartsCars.Add(new PartCar
                        {
                            PartId = partID
                        });
                    }
                }
                cars.Add(currentCar);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        //12.
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            Customer[] customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}.";
        }

        //13.
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            Sale[] sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}.";
        }

        //14.
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.Date,
                    IsYoungDriver = c.IsYoungDriver
                })
                .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                .ToArray();

            var customersFormatted = customers
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver
                });


            string result = JsonConvert.SerializeObject(customersFormatted, Formatting.Indented);

            return result;
        }

        //15.
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TraveledDistance
                })
                .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TraveledDistance);

            string result = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return result;
        }

        //16.
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                });

            string result = JsonConvert.SerializeObject (suppliers, Formatting.Indented);

            return result;
        }

        //17.
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsInfo = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance
                    },
                    parts = c.PartsCars.Select(c => new
                    {
                        Name = c.Part.Name,
                        Price = c.Part.Price.ToString("f2") 
                    }).ToArray()
                })
                .ToArray();

            string result = JsonConvert.SerializeObject(carsInfo, Formatting.Indented);

            return result;
        }

        //18.
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales
                        .Select(x => x.Car.PartsCars.Sum(pc => pc.Part.Price))
                })
                .ToArray();

            var customerFormatted = customers
                .Select(c => new
                {
                    c.fullName,
                    c.boughtCars,
                    spentMoney = c.spentMoney.Sum()
                })
                .OrderByDescending(c => c.spentMoney)
                    .ThenByDescending(c => c.boughtCars);

            string result = JsonConvert.SerializeObject (customerFormatted, Formatting.Indented);

            return result;
        }

        //19.
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TraveledDistance
                    },
                    customerName = s.Customer.Name,
                    discount = s.Discount.ToString("f2"),
                    price = s.Car.PartsCars.Sum(pc => pc.Part.Price).ToString("f2"),
                    priceWithDiscount = 
                    (s.Car.PartsCars.Sum(pc => pc.Part.Price) * (1 - (s.Discount / 100))).ToString("f2")
                })
                .Take(10);

            string result = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return result;
        }
    }
}