namespace ForumApp.Data.Constants
{
    public static class ValidationConstants
    {
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 10;

        public const int ContentMaxLength = 1500;
        public const int ContentMinLength = 30;


        public const string RequireErrorMessage = "The field {0} is required";

        public const string StringLengthErrorMessage = "The {0} field must be between {2} and {1} characters long";
    }
}
