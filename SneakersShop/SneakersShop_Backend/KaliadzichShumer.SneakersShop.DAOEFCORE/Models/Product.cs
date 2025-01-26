using KaliadzichShumer.SneakersShop.INTERFACES.Models;

namespace KaliadzichShumer.SneakersShop.DAOEFCORE.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public virtual Producer Producer { get; set; }
    }
} 