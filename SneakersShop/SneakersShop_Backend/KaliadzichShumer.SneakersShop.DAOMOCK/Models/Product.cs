using KaliadzichShumer.SneakersShop.INTERFACES.Models;

namespace KaliadzichShumer.SneakersShop.DAOMOCK.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
    }
} 