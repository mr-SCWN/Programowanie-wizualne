using KaliadzichShumer.SneakersShop.INTERFACES;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace KaliadzichShumer.SneakersShop.DAO.File
{
    public class FileDAO : IDAO
    {
        private readonly string manufacturersFile = "manufacturers.json";
        private readonly string sneakersFile = "sneakers.json";

        private List<DataObjects.ManufacturerDO> manufacturers;
        private List<DataObjects.SneakerDO> sneakers;

        public FileDAO()
        {
            manufacturers = LoadData<DataObjects.ManufacturerDO>(manufacturersFile);
            sneakers = LoadData<DataObjects.SneakerDO>(sneakersFile);
        }

        public IEnumerable<object> GetAllManufacturers() => manufacturers;
        public void AddManufacturer(object manufacturer)
        {
            var m = manufacturer as DataObjects.ManufacturerDO;
            m.Id = manufacturers.Any()? manufacturers.Max(x=>x.Id)+1 : 1;
            manufacturers.Add(m);
            SaveData(manufacturersFile, manufacturers);
        }

        public void UpdateManufacturer(object manufacturer)
        {
            var m = manufacturer as DataObjects.ManufacturerDO;
            var found = manufacturers.FirstOrDefault(x=>x.Id == m.Id);
            if(found != null)
            {
                found.Name = m.Name;
                SaveData(manufacturersFile, manufacturers);
            }
        }

        public void DeleteManufacturer(int manufacturerId)
        {
            var found = manufacturers.FirstOrDefault(x=>x.Id == manufacturerId);
            if(found != null)
            {
                manufacturers.Remove(found);
                SaveData(manufacturersFile, manufacturers);
            }
        }

        public IEnumerable<object> GetAllSneakers() => sneakers;
        public void AddSneaker(object sneaker)
        {
            var s = sneaker as DataObjects.SneakerDO;
            s.Id = sneakers.Any()? sneakers.Max(x=>x.Id)+1 : 1;
            sneakers.Add(s);
            SaveData(sneakersFile, sneakers);
        }

        public void UpdateSneaker(object sneaker)
        {
            var s = sneaker as DataObjects.SneakerDO;
            var found = sneakers.FirstOrDefault(x=>x.Id==s.Id);
            if(found != null)
            {
                found.ModelName = s.ModelName;
                found.ManufacturerId = s.ManufacturerId;
                found.Category = s.Category;
                SaveData(sneakersFile, sneakers);
            }
        }

        public void DeleteSneaker(int sneakerId)
        {
            var found = sneakers.FirstOrDefault(x=>x.Id==sneakerId);
            if(found != null)
            {
                sneakers.Remove(found);
                SaveData(sneakersFile, sneakers);
            }
        }

        private List<T> LoadData<T>(string filePath)
        {
            if(!System.IO.File.Exists(filePath))
                return new List<T>();
            var json = System.IO.File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        private void SaveData<T>(string filePath, List<T> data)
        {
            var json = JsonSerializer.Serialize(data);
            System.IO.File.WriteAllText(filePath, json);
        }
    }
}
