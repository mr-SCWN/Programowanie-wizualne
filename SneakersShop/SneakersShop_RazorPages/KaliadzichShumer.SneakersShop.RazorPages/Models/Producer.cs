namespace KaliadzichShumer.SneakersShop.RazorPages.Models{
    public class Producer  {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}
