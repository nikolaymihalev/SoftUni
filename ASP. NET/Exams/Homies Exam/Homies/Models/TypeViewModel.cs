namespace Homies.Models
{
    public class TypeViewModel
    {
        public TypeViewModel(int id,string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
