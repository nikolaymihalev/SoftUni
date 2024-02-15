namespace SoftUniBazar.Models
{
    public class AdInfoViewModel
    {
        public AdInfoViewModel(int id,
            string name,
            string imageUrl,
            string createdOn,
            string category,
            string description,
            string price,
            string owner)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            CreatedOn = createdOn;
            Category = category;
            Description = description;
            Price = price;
            Owner = owner;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedOn { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Owner { get; set; }
    }
}
