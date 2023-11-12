namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Globalization;
    using System.Net.Mime;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            Console.WriteLine(GetBooksByCategory(db, "horror mystery drama"));
        }

        //02.
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out AgeRestriction ageRestriction))
            {
                return $"{command} is not a valid type!";
            }

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();


            return string.Join(Environment.NewLine, books
                    .OrderBy(b => b.Title)
                    .Select(b => b.Title));
        }

        //03.
        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold
                    && b.Copies < 5000)
                .Select(b => new
                {
                    b.Title,
                    b.BookId
                })
                .ToList();

            return string.Join(Environment.NewLine, goldenBooks
                .OrderBy(b => b.BookId)
                .Select(b => b.Title));
        }

        //04.
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price >= 40.00m)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                });

            return string.Join(Environment.NewLine, books
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}"));
        }

        //05.
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var nonReleasedBooks = context.Books
                .Where(b => b.ReleaseDate.HasValue
                    && b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.Title,
                    b.BookId
                })
                .ToList();

            return string.Join(Environment.NewLine, nonReleasedBooks
                .OrderBy(b => b.BookId)
                .Select(b => b.Title));
        }

        //06.
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var booksByCategories = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name)))
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            return string.Join(Environment.NewLine, booksByCategories
                    .OrderBy(b => b.Title)
                    .Select(b => b.Title));
        }

        //07.
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime parsedDate = 

            //var booksBeforeGivenYear = context.Books
            //    .Where(b => b.ReleaseDate.HasValue
            //        && b.ReleaseDate.Value.Year < date.)

            return string.Empty;
        }
    }
}


