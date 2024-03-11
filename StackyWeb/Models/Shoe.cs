namespace StackyWeb.Models
{
    public class Shoe
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int QuantityAvailable { get; set; }
        public string UrlImg { get; set; }
    }
}
