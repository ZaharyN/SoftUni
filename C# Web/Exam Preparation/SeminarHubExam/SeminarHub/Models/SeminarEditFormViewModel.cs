namespace SeminarHub.Models
{
    public class SeminarEditFormViewModel
    {
        public int Id { get; set; }

        public string Topic { get; set; } = string.Empty;

        public string Lecturer { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public string DateAndTime { get; set; } = string.Empty;

        public int Duration { get; set; }

        public int CategoryId { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
