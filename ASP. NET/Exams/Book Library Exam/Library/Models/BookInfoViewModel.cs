namespace Library.Models
{
    public class BookInfoViewModel
    {
        public BookInfoViewModel(
            int id,
            string title,
            string author,
            string imageUrl,
            string rating,
            string category,
            string description)
        {
            Id = id;
            Title = title;
            Author = author;
            ImageUrl = imageUrl;
            Rating = rating;
            Category = category;
            Description = description;

        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Rating { get; set; }
        public string Category { get; set; }
    }
}
