using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaliadzichShumer.SneakersShop.DAO.SQL.DataObjects
{
    [Table("Manufacturers")]
    public class ManufacturerDO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
