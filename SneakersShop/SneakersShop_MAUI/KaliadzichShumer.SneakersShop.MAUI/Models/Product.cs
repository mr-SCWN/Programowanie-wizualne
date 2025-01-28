namespace KaliadzichShumer.SneakersShop.MAUI.Models  {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public  int ProducerId { get; set; }
        public string ProducerName { get; set; } = string.Empty;
        public  string ShoeType  {get;set; } = string.Empty ;
    }
}
