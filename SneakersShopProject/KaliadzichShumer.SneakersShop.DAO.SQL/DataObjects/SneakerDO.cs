using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KaliadzichShumer.SneakersShop.CORE.Enums;

namespace KaliadzichShumer.SneakersShop.DAO.SQL.DataObjects
{
    [Table("Sneakers")]
    public class SneakerDO
    {
        [Key]
        public int Id { get; set; }
        public string ModelName { get; set; }
        public CategoryEnum Category { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public ManufacturerDO Manufacturer { get; set; }
    }
}
