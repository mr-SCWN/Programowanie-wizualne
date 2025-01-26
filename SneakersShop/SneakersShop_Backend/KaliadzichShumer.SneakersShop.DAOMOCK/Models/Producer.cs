using KaliadzichShumer.SneakersShop.INTERFACES.Models;

namespace KaliadzichShumer.SneakersShop.DAOMOCK.Models {
    public class Producer : IProducer {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<IProduct> Products { get; set; } = new List<IProduct>();
    }
} 