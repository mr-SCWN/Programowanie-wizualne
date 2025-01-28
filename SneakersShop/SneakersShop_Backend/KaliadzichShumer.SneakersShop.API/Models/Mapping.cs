using KaliadzichShumer.SneakersShop.INTERFACES.Models;
using System;


namespace KaliadzichShumer.SneakersShop.API.Models {
    public static class Mapping  {
        public static ProducerDto ToDto(this IProducer producer) {
            return new ProducerDto {
                Id = producer.Id,
                Name = producer.Name,
                Country=producer.Country
            };
        }

        public static IProducer ToModel(this ProducerDto dto) {
            return new ProducerModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Country=dto.Country
            };
        }

        public static ProductDto ToDto(this IProduct product) {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ProducerId = product.ProducerId,
                ProducerName = product.ProducerName,
                ShoeType =product.ShoeType.ToString()
            };
        }

        public static IProduct ToModel(this ProductDto dto) 
        {
            if (Enum.TryParse<ShoeType>(dto.ShoeType, ignoreCase: true, out var shoeTypeValue))
            {
            return new ProductModel {
                Id = dto.Id,
                Name = dto.Name,
                ProducerId = dto.ProducerId,
                ProducerName  = dto.ProducerName,
                ShoeType= shoeTypeValue

            };
            } else {
                throw new ArgumentException("Invalid ShoeType value");
            }
        }
    }

    internal class ProducerModel : IProducer{
        public int Id {get;set;}
        public string Name { get; set; }
        public string Country {get; set;}
        public ICollection<IProduct> Products { get; set; } = new List<IProduct>();
    }

    internal class ProductModel : IProduct{
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public ShoeType ShoeType {get;set;}
    }
} 