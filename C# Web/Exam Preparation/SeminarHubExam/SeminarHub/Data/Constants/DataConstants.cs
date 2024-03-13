using Humanizer.Localisation.TimeToClockNotation;

namespace SeminarHub.Data.Constants
{
    public static class DataConstants
    {
        public const int SeminarTopicMinLength = 3;
        public const int SeminarTopicMaxLength = 100;

        public const int SeminarLecturerMinLength = 5;
        public const int SeminarLecturerMaxLength = 60;

        public const int SeminarDetailsMinLength = 10;
        public const int SeminarDetailsMiaxLength = 500;

        public const string SeminarDateTimeFormat = "dd/MM/yyyy HH:mm";

        public const int SeminarDurationMinLength = 30;
        public const int SeminarDurationMaxLength = 180;

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 50;
    }
}
