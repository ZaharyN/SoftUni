using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Homies.Data.Constants.DataConstants;

namespace Homies.Models.EventViewModels
{
    public class EventViewModel
    {
        public EventViewModel(int id, string name, DateTime startDate, string type, string organiser)
        {
            Id = id;
            Name = name;
            Start = startDate.ToString(DateTimeFormat);
            Type = type;
            Organiser = organiser;
        }
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Start { get; set; }

        public string Type { get; set; }
        public string Organiser { get; set; }

    }
}
