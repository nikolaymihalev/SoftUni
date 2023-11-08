namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Globalization;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
        }

        //02
        public static string GetBooksByAgeRestriction(BookShopContext context, string command) 
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction)) 
            {
                return $"{command} is not a valid age restriction";
            }
            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => new 
                { 
                    b.Title 
                })
                .OrderBy(b=>b.Title)
                .ToList();


            return String.Join(Environment.NewLine, books.Select(b=>b.Title));
        }

        //03
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //04
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price>40)
                .Select(b =>new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b=>b.Price)
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b => $"{b.Title} ${b.Price:f2}"));
        }

        //05
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Select(b => new 
                {
                    b.Title,
                    b.ReleaseDate
                })
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year != year)
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //06
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories= input.Split(' ',StringSplitOptions.RemoveEmptyEntries)
                                        .Select(c=>c.ToLower())
                                        .ToArray();

            var books = context.Books
                .Where(b => b.BookCategories.Any(bc=> categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => new
                {
                    b.Title,       
                    b.BookCategories
                })
                .OrderBy(b => b.Title)
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b => b.Title));
        }


        //07
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy",CultureInfo.InvariantCulture);

            var books = context.Books
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .Where(b => b.ReleaseDate < parsedDate)
                .OrderByDescending(b => b.ReleaseDate);

            return String.Join(Environment.NewLine, books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}"));
        }

        //08
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a=> a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName= a.FirstName+" "+a.LastName
                })
                .OrderBy(a=>a.FullName)
                .ToList();

            return String.Join(Environment.NewLine, authors.Select(a=>a.FullName));
        }

        //09
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.Contains(input))
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //10
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Author.LastName.StartsWith(input))
                .Select(b => new
                {
                    BookTitle= b.Title,
                    AuthorName= b.Author.FirstName+" "+b.Author.LastName
                })
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b => $"{b.BookTitle} ({b.AuthorName})"));
        }

        //11
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books
                .Count(b => b.Title.Length > lengthCheck);
        }

        //12
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    AuthorFullName = a.FirstName + " " + a.LastName,
                    TotalBooks = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a=>a.TotalBooks)
                .ToList();

            return String.Join(Environment.NewLine, authors.Select(a => $"{a.AuthorFullName} - {a.TotalBooks}"));
        }

        //13
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profit = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Sum(x=>x.Book.Copies * x.Book.Price)
                })
                .OrderByDescending(c=>c.TotalProfit)
                .ThenBy(c=>c.CategoryName)
                .ToList();

            return String.Join(Environment.NewLine, profit.Select(c => $"{c.CategoryName} ${c.TotalProfit:F2}"));
        }

        //14
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var booksCategories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    MostRecentBooks = c.CategoryBooks
                                       .OrderByDescending(x => x.Book.ReleaseDate)
                                       .Take(3)
                                       .Select(cb => new
                                       {
                                           BookTitle = cb.Book.Title,
                                           cb.Book.ReleaseDate.Value.Year
                                       })
                })
                .OrderBy(c => c.CategoryName);

            StringBuilder sb = new StringBuilder();

            foreach (var bookCat in booksCategories) 
            {
                sb.AppendLine($"--{bookCat.CategoryName}");
                foreach (var book in bookCat.MostRecentBooks) 
                {
                    sb.AppendLine($"{book.BookTitle} ({book.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }
        //15
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }
            context.SaveChanges();
        }

    }
}


