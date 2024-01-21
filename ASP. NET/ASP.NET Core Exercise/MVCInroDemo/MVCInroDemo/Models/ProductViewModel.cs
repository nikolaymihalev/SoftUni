namespace MVCInroDemo.Models
{
    /// <summary>
    /// Product model
    /// </summary>
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
