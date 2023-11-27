namespace Trucks.DataProcessor
{
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using System.Text;
    using System.Xml.Serialization;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            //MapperConfiguration configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<TrucksProfile>();
            //});

            DispatcherWithTrucksDTO[] dispatchers = context.Despatchers
                .Where(d => d.Trucks.Any())
                .ToArray()
                .Select(d => new DispatcherWithTrucksDTO
                {
                    TrucksCount = d.Trucks.Count,
                    Name = d.Name,
                    Trucks = d.Trucks.Select(t => new ExportTruckDTO
                    {
                        RegistrationNumber = t.RegistrationNumber,
                        Make = t.MakeType.ToString()
                    })
                    .OrderBy(t => t.RegistrationNumber)
                    .ToArray()
                })
                .OrderByDescending(d => d.TrucksCount)
                    .ThenBy(d => d.Name)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(DispatcherWithTrucksDTO[]),
                new XmlRootAttribute("Despatchers"));
            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();

            xmlns.Add(string.Empty, string.Empty);

            StringBuilder sb = new();

            using(StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, dispatchers, xmlns);
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clients = context.Clients
                .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
                .ToArray()
                .Select(c => new
                {
                    Name = c.Name,
                    Trucks = c.ClientsTrucks
                    .Where(c => c.Truck.TankCapacity >= capacity)
                    .Select(ct => new
                    {
                        TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                        VinNumber = ct.Truck.VinNumber,
                        TankCapacity = ct.Truck.TankCapacity,
                        CargoCapacity = ct.Truck.CargoCapacity,
                        CategoryType = ct.Truck.CategoryType.ToString(),
                        MakeType = ct.Truck.MakeType.ToString()
                    })
                    .OrderBy(ct => ct.MakeType)
                        .ThenByDescending(ct => ct.CargoCapacity)
                    .ToArray()
                })
                .OrderByDescending(c => c.Trucks.Length)
                    .ThenBy(c => c.Name)
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(clients, Formatting.Indented);
        }
    }
}
