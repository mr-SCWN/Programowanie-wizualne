using Microsoft.EntityFrameworkCore;
using KaliadzichShumer.SneakersShop.INTERFACES;
using KaliadzichShumer.SneakersShop.INTERFACES.Models;
using KaliadzichShumer.SneakersShop.DAOEFCORE.Models;

namespace KaliadzichShumer.SneakersShop.DAOEFCORE {
    public class EFCOREDAO : IDAO  {
        private readonly SneakersShopContext _context;

        public EFCOREDAO(string connectionString) {
            _context = new SneakersShopContext(connectionString);
            _context.Database.EnsureCreated();
        }

        public IEnumerable<IProducer> GetAllProducers() {
            return _context.Producers.Include(p => p.ProductsCollection).ToList();
        }

        public IProducer GetProducerById(int id) {
            return _context.Producers.Include(p => p.ProductsCollection).FirstOrDefault(p => p.Id == id);
        }

        public IProducer CreateProducer(IProducer producer) {
            var efProducer = new Producer
            {
                Name = producer.Name,
                ProductsCollection = new List<Product>()
            };
            _context.Producers.Add(efProducer);
            _context.SaveChanges();
            return efProducer;
        }

        public void UpdateProducer(IProducer producer) {
            var efProducer = _context.Producers.Find(producer.Id);
            if (efProducer != null) {
                efProducer.Name = producer.Name;
                _context.SaveChanges();
            }
        }

        public void DeleteProducer(int id) {
            var producer = _context.Producers.Find(id);
            if (producer != null) {
                _context.Producers.Remove(producer);
                _context.SaveChanges();
            }
        }

        public IEnumerable<IProduct> GetAllProducts()  {
            return _context.Products.ToList();
        }

        public IProduct GetProductById(int id) {
            return _context.Products.Find(id);
        }

        public IProduct CreateProduct(IProduct product) {
            var _PRODUCT = new Product
            {
                Name = product.Name,
                ProducerId = product.ProducerId
            };
            
            var producer = _context.Producers.Find(product.ProducerId);
            if (producer != null) {

                _PRODUCT.ProducerName = producer.Name;
                _context.Products.Add(_PRODUCT);

                _context.SaveChanges();
            }
            return _PRODUCT;
        }

        public void UpdateProduct(IProduct product) {

            var efProduct = _context.Products.Find(product.Id);
            if (efProduct != null) {
                efProduct.Name = product.Name;
                efProduct.ProducerId = product.ProducerId;
                
                var producer = _context.Producers.Find(product.ProducerId);
                if (producer != null) {
                    efProduct.ProducerName = producer.Name; _context.SaveChanges();
                }
            }
        }

        public void DeleteProduct(int id)  {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product); _context.SaveChanges();
            }
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
} 