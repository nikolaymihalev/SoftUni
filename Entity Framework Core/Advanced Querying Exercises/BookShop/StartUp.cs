namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

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
    }
}


