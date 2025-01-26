using KaliadzichShumer.SneakersShop.INTERFACES;
using KaliadzichShumer.SneakersShop.INTERFACES.Models;
using KaliadzichShumer.SneakersShop.INTERFACES.Services;

namespace KaliadzichShumer.SneakersShop.BLC.Services
{
    public class SneakersShopService : ISneakersShopService
    {
        private readonly IDAO _dao;

        public SneakersShopService(IDAO dao)
        {
            _dao = dao;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _dao.GetAllProducers();
        }
        
        public IProducer GetProducerById(int id)
        {
            return _dao.GetProducerById(id);
        }
        
        public IProducer CreateProducer(IProducer producer)
        {
            var created = _dao.CreateProducer(producer);
            _dao.SaveChanges();
            return created;
        }
        
        public void UpdateProducer(IProducer producer)
        {
            _dao.UpdateProducer(producer);
            _dao.SaveChanges();
        }
        
        public void DeleteProducer(int id)
        {
            _dao.DeleteProducer(id);
            _dao.SaveChanges();
        }

        public IEnumerable<IProduct> GetAllProducts()
        {
            return _dao.GetAllProducts();
        }
        
        public IProduct GetProductById(int id)
        {
            return _dao.GetProductById(id);
        }
        
        public IProduct CreateProduct(IProduct product)
        {
            var created = _dao.CreateProduct(product);
            _dao.SaveChanges();
            return created;
        }
        
        public void UpdateProduct(IProduct product)
        {
            _dao.UpdateProduct(product);
            _dao.SaveChanges();
        }
        
        public void DeleteProduct(int id)
        {
            _dao.DeleteProduct(id);
            _dao.SaveChanges();
        }
    }
} 