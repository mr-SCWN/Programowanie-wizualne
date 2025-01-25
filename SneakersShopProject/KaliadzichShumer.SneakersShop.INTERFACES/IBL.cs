using System.Collections.Generic;

namespace KaliadzichShumer.SneakersShop.INTERFACES
{
    public interface IBL
    {
        void AddManufacturer(object manufacturer);
        void UpdateManufacturer(object manufacturer);
        void DeleteManufacturer(int manufacturerId);
        IEnumerable<object> GetAllManufacturers();

        void AddSneaker(object sneaker);
        void UpdateSneaker(object sneaker);
        void DeleteSneaker(int sneakerId);
        IEnumerable<object> GetAllSneakers();
    }
}
