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
            //DbInitializer.ResetDatabase(db);

            string input=Console.ReadLine();
            Console.WriteLine(GetBooksByAgeRestriction(db,input));
        }
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
    }
}


