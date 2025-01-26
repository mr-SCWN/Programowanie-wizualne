namespace KaliadzichShumer.SneakersShop.INTERFACES.Models
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        int ProducerId { get; set; }
        string ProducerName { get; set; }
    }
} 