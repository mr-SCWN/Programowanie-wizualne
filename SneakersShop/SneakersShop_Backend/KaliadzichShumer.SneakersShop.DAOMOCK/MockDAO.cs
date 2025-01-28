using KaliadzichShumer.SneakersShop.INTERFACES;
using KaliadzichShumer.SneakersShop.INTERFACES.Models;
using KaliadzichShumer.SneakersShop.DAOMOCK.Models;

namespace KaliadzichShumer.SneakersShop.DAOMOCK
{
    public class MockDAO : IDAO
    {
        private List<IProducer> _producers;
        private List<IProduct> _products;

        public MockDAO()
        {
            _producers = new List<IProducer>();
            _products = new List<IProduct>();

            var producer1 = new Producer
            {
                Id = 1,
                Name = "Producer 1",
                Country = "USA",
                Products = new List<IProduct>()
            };
            
            var producer2 = new Producer
            {
                Id = 2,
                Name = "Producer 2",
                Country = "Germany",
                Products = new List<IProduct>()
            };

            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                ProducerId = producer1.Id,
                ProducerName = producer1.Name,
                ShoeType = ShoeType.Running
            };
            
            var product2 = new Product
            {
                Id = 2,
                Name = "Product 2",
                ProducerId = producer2.Id,
                ProducerName = producer2.Name,
                ShoeType = ShoeType.Basketball
            };

            producer1.Products.Add(product1);
            producer2.Products.Add(product2);

            _producers.Add(producer1);
            _producers.Add(producer2);
            _products.Add(product1);
            _products.Add(product2);
        }


        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }


        public IProducer GetProducerById(int id)
        {
            return _producers.FirstOrDefault(p => p.Id == id);
        }



        public IProducer CreateProducer(IProducer producer)
        {
            var newProducer = new Producer
            {
                Id = _producers.Any() ? _producers.Max(p => p.Id) + 1 : 1,
                Name = producer.Name,
                Country = producer.Country,
                Products = new List<IProduct>()
            };
            _producers.Add(newProducer);
            return newProducer;
        }



        public void UpdateProducer(IProducer producer)
        {
            var existing = _producers.FirstOrDefault(p => p.Id == producer.Id);
            if (existing != null)
            {
                existing.Name = producer.Name;
                existing.Country  = producer.Country; 
                foreach (var product in _products.Where(p => p.ProducerId == producer.Id))
                {
                    product.ProducerName = producer.Name;
                }
            }
        }



        public void DeleteProducer(int id)
        {
            var producer = _producers.FirstOrDefault(p => p.Id == id);
            if (producer != null)
            {
                _producers.Remove(producer);
                _products.RemoveAll(p => p.ProducerId == id);
            }
        }



        public IEnumerable<IProduct> GetAllProducts()
        {
            return _products;
        }

        public IProduct GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }



        public IProduct CreateProduct(IProduct product)
        {
            var producer = _producers.FirstOrDefault(p => p.Id == product.ProducerId);
            if (producer != null)
            {
                var newProduct = new Product
                {
                    Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1,
                    Name = product.Name,
                    ProducerId = product.ProducerId,
                    ProducerName = producer.Name ,
                    ShoeType = product.ShoeType
                };
                _products.Add(newProduct);
                producer.Products.Add(newProduct);
                return newProduct;
            }
            return null;
        }



        public void UpdateProduct(IProduct product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                var oldProducer = _producers.FirstOrDefault(p => p.Id == existing.ProducerId);
                var newProducer = _producers.FirstOrDefault(p => p.Id == product.ProducerId);

                if (oldProducer != null)
                    oldProducer.Products.Remove(existing);

                if (newProducer != null)
                {
                    existing.Name = product.Name;
                    existing.ProducerId = product.ProducerId;
                    existing.ProducerName = newProducer.Name;
                    existing.ShoeType = product.ShoeType;
                    newProducer.Products.Add(existing);
                }
            }
        }



        public void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                var producer = _producers.FirstOrDefault(p => p.Id == product.ProducerId);
                if (producer != null)
                {
                    producer.Products.Remove(product);
                }
            }
        }

        public void SaveChanges(){}
    }
} 