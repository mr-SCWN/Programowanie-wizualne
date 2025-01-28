namespace KaliadzichShumer.SneakersShop.API.Models{
    public enum ShoeTypeDto{
    Running,
    Walking,
    Football,
    Basketball
    }

    public class ProductDto{
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public string  ShoeType {get; set;}
    }
} 