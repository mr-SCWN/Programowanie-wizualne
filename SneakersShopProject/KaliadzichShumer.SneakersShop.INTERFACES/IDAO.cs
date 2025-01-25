using System.Collections.Generic;

namespace KaliadzichShumer.SneakersShop.INTERFACES
{
    public interface IDAO
    {
        // Manufacturer 
        IEnumerable<object> GetAllManufacturers();
        void AddManufacturer(object manufacturer);
        void UpdateManufacturer(object manufacturer);
        void DeleteManufacturer(int manufacturerId);

        // Sneakers 
        IEnumerable<object> GetAllSneakers();
        void AddSneaker(object sneaker);
        void UpdateSneaker(object sneaker);
        void DeleteSneaker(int sneakerId);
    }
}
