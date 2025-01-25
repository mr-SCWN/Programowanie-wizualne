using KaliadzichShumer.SneakersShop.INTERFACES;
using System.Collections.Generic;
using System.Linq;

namespace KaliadzichShumer.SneakersShop.DAO.Mock
{
    public class MockDAO : IDAO
    {
        private List<DataObjects.ManufacturerDO> manufacturers;
        private List<DataObjects.SneakerDO> sneakers;

        public MockDAO()
        {
            // Initialization data
            manufacturers = new List<DataObjects.ManufacturerDO>()
            {
                new DataObjects.ManufacturerDO{ Id=1, Name="Nike" },
                new DataObjects.ManufacturerDO{ Id=2, Name="Adidas" }
            };

            sneakers = new List<DataObjects.SneakerDO>()
            {
                new DataObjects.SneakerDO{ Id=1, ManufacturerId=1, ModelName="Air Max", Category=CORE.Enums.CategoryEnum.Running },
                new DataObjects.SneakerDO{ Id=2, ManufacturerId=2, ModelName="Ultraboost", Category=CORE.Enums.CategoryEnum.Running }
            };
        }

        // Realization IDAO:
        public IEnumerable<object> GetAllManufacturers() => manufacturers;

        public void AddManufacturer(object manufacturer)
        {
            var m = manufacturer as DataObjects.ManufacturerDO;
            m.Id = manufacturers.Any() ? manufacturers.Max(x=>x.Id) + 1 : 1;
            manufacturers.Add(m);
        }

        public void UpdateManufacturer(object manufacturer)
        {
            var m = manufacturer as DataObjects.ManufacturerDO;
            var found = manufacturers.FirstOrDefault(x=>x.Id == m.Id);
            if(found != null)
            {
                found.Name = m.Name;
            }
        }

        public void DeleteManufacturer(int manufacturerId)
        {
            var found = manufacturers.FirstOrDefault(x=>x.Id == manufacturerId);
            if(found != null) manufacturers.Remove(found);
        }

        public IEnumerable<object> GetAllSneakers() => sneakers;

        public void AddSneaker(object sneaker)
        {
            var s = sneaker as DataObjects.SneakerDO;
            s.Id = sneakers.Any()? sneakers.Max(x=>x.Id)+1 : 1;
            sneakers.Add(s);
        }

        public void UpdateSneaker(object sneaker)
        {
            var s = sneaker as DataObjects.SneakerDO;
            var found = sneakers.FirstOrDefault(x=>x.Id == s.Id);
            if(found != null)
            {
                found.ModelName = s.ModelName;
                found.ManufacturerId = s.ManufacturerId;
                found.Category = s.Category;
            }
        }

        public void DeleteSneaker(int sneakerId)
        {
            var found = sneakers.FirstOrDefault(x=>x.Id == sneakerId);
            if(found != null) sneakers.Remove(found);
        }
    }
}
