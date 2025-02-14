namespace KaliadzichShumer.SneakersShop.INTERFACES.Models {
    public interface IProducer {
        int Id { get; set; }
        string Name { get; set; }
        string Country {get ; set;}
        ICollection<IProduct> Products { get; set; }
    }
} 