namespace KaliadzichShumer.SneakersShop.RazorPages.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; } 
    }
}
