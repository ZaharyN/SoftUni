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
            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue
                && b.ReleaseDate.Value < parsedDate)
                .Select(b => new
                {
                    b.Title,
                    EditionType = b.EditionType.ToString(),
                    b.Price,
                    b.ReleaseDate
                })
                .ToList();
        
            return string.Join(Environment.NewLine, books.OrderByDescending(b => b.ReleaseDate)
                    .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}"));
        }
        
        //08.
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();
        
            return string.Join(Environment.NewLine, authors
                    .OrderBy(a => a.FullName)
                    .Select(a => a.FullName));
        }
        
        //09.
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => new
                {
                    b.Title
                })
                .ToList();
        
            return string.Join(Environment.NewLine, books
                .OrderBy(b => b.Title)
                .Select(b => b.Title));
        }

         //10.
         public static string GetBooksByAuthor(BookShopContext context, string input)
         {
             var books = context.Books
                 .Include(b => b.Author)
                 .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                 .Select(b => new
                 {
                     b.Title,
                     AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                     b.BookId
                 })
                 .ToList();
        
             return string.Join(Environment.NewLine, books
                 .OrderBy(b => b.BookId)
                 .Select(b => $"{b.Title} ({b.AuthorName})"));
         }
        
         //11.
         public static int CountBooks(BookShopContext context, int lengthCheck)
         {
             var count = context.Books
                 .Where(b => b.Title.Length > lengthCheck)
                 .Count();
        
             return count;
         }
        
         //12.
         public static string CountCopiesByAuthor(BookShopContext context)
         {
             var authors = context.Authors
                 .Include(a => a.Books)
                 .Select(a => new
                 {
                     FullName = a.FirstName + " " + a.LastName,
                     Copies = a.Books.Select(a => new
                     {
                         a.Copies
                     }).Sum(c => c.Copies)
                 })
                 .ToList();
                 
             return string.Join(Environment.NewLine, authors
                     .OrderByDescending(a => a.Copies)
                     .Select(a => $"{a.FullName} - {a.Copies}"));
         }
        
         //13.
         public static string GetTotalProfitByCategory(BookShopContext context)
         {
             var categories = context.Categories
                 .Include(c => c.CategoryBooks)
                     .ThenInclude(c => c.Book)
                 .Select(c => new
                 {
                     c.Name,
                     TotalProfitPerBook = c.CategoryBooks.Select(cb => new
                     {
                         ProfitPerBook = cb.Book.Price * cb.Book.Copies
                     })
                     .Sum(cb => cb.ProfitPerBook)
                 })
                 .ToList();
        
             return string.Join(Environment.NewLine, categories
                     .OrderByDescending(c => c.TotalProfitPerBook)
                         .ThenBy(c => c.Name)
                     .Select(c => $"{c.Name} ${c.TotalProfitPerBook:f2}"));
                     
         }
    }
}


