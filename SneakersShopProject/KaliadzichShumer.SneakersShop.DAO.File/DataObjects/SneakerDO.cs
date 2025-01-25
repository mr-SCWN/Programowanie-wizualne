using KaliadzichShumer.SneakersShop.CORE.Enums;

namespace KaliadzichShumer.SneakersShop.DAO.File.DataObjects
{
    public class SneakerDO
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int ManufacturerId { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
