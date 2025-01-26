using KaliadzichShumer.SneakersShop.INTERFACES.Models;

namespace KaliadzichShumer.SneakersShop.INTERFACES.Services {
    public interface ISneakersShopService {
        IEnumerable<IProducer> GetAllProducers();
        IProducer GetProducerById(int id);
        IProducer CreateProducer(IProducer producer);
        void UpdateProducer(IProducer producer);
        void DeleteProducer(int id);

        IEnumerable<IProduct> GetAllProducts();
        IProduct GetProductById(int id);
        IProduct CreateProduct(IProduct product);
        void UpdateProduct(IProduct product);
        void DeleteProduct(int id);
    }
} 