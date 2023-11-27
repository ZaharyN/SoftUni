using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Data.Models;

namespace Trucks.Data.Models
{
    public class ClientTruck
    {
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }


        [ForeignKey(nameof(Truck))]
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
    }
}

//•	ClientId – integer, Primary Key, foreign key (required)
//•	Client – Client
//•	TruckId – integer, Primary Key, foreign key (required)
//•	Truck – Truck
