using System.Collections.Generic;
using KaliadzichShumer.SneakersShop.MAUI.Models;

namespace KaliadzichShumer.SneakersShop.MAUI.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
