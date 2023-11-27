namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            StringBuilder result = new();
            XmlSerializer serializer = new XmlSerializer(typeof(DespatcherWithTrucksDTO[]),
                new XmlRootAttribute("Despatchers"));
            using StringReader reader = new StringReader(xmlString);

            DespatcherWithTrucksDTO[] despatchersTrucksDTO = serializer.Deserialize(reader) as DespatcherWithTrucksDTO[];

            List<Despatcher> despatchers = new List<Despatcher>();

            foreach (DespatcherWithTrucksDTO despatcherDTO in despatchersTrucksDTO)
            {
                if (!IsValid(despatcherDTO))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                string position = despatcherDTO.Position;

                if(string.IsNullOrEmpty(position))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Despatcher despatcher = new Despatcher
                {
                    Name = despatcherDTO.Name,
                    Position = position
                };

                foreach (ImportTruckDTO truck in despatcherDTO.Trucks)
                {
                    
                    if (!IsValid(truck))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    Truck currentTruck = new Truck
                    {
                        RegistrationNumber = truck.RegistrationNumber,
                        VinNumber = truck.VinNumber,
                        TankCapacity = truck.TankCapacity,
                        CargoCapacity = truck.CargoCapacity,
                        CategoryType = (CategoryType)truck.CategoryType,
                        MakeType = (MakeType)truck.MakeType
                    };

                    despatcher.Trucks.Add(currentTruck);
                }

                despatchers.Add(despatcher);
                result.AppendLine(String.Format(SuccessfullyImportedDespatcher, despatcher.Name, despatcher.Trucks.Count));
            }
            context.Despatchers.AddRange(despatchers);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder result = new();
            ClientWithTrucksDTO[] clientsWithTrucks = JsonConvert.DeserializeObject<ClientWithTrucksDTO[]>(jsonString);

            List<Client> clients = new List<Client>();

            foreach (ClientWithTrucksDTO client in clientsWithTrucks)
            {
                if (!IsValid(client))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }   
                if(client.Type == "usual")
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Client currentClient = new Client
                {
                    Name = client.Name,
                    Nationality = client.Nationality,
                    Type = client.Type,
                };

                foreach (int truckId in client.Trucks.Distinct())
                {
                    if(!context.Trucks.Any(t => t.Id == truckId))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    currentClient.ClientsTrucks.Add(new ClientTruck
                    {
                        TruckId = truckId
                    });
                }

                clients.Add(currentClient);
                result.AppendLine(string.Format(SuccessfullyImportedClient, currentClient.Name, currentClient.ClientsTrucks.Count));
            }
            context.Clients.AddRange(clients);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}