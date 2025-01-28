using System.Collections.Generic;
using KaliadzichShumer.SneakersShop.INTERFACES.Models;

namespace KaliadzichShumer.SneakersShop.DAOEFCORE.Models
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country {get; set; }
        public virtual ICollection<Product> ProductsCollection { get; set; } = new List<Product>();
        ICollection<IProduct> IProducer.Products 
        { 
            get => ProductsCollection.Cast<IProduct>().ToList();
            set => ProductsCollection = value.Cast<Product>().ToList();
        }
    }
} 