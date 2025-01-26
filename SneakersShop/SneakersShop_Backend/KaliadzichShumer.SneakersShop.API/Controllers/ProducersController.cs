using Microsoft.AspNetCore.Mvc;
using System.Linq;

using KaliadzichShumer.SneakersShop.INTERFACES.Services;
using KaliadzichShumer.SneakersShop.API.Models;


namespace KaliadzichShumer.SneakersShop.API.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducersController : ControllerBase {

        private readonly ISneakersShopService _service;

        public ProducersController(ISneakersShopService service) { _service = service; }

        [HttpGet]
        public IActionResult GetAll(){
            var producers = _service.GetAllProducers();
            return Ok(producers.Select(p => p.ToDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var producer = _service.GetProducerById(id);
            if (producer == null){
                return NotFound();
            }

            return Ok(producer.ToDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProducerDto producerDto){
            var producer = producerDto.ToModel();
            var created = _service.CreateProducer(producer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProducerDto producerDto){
            if (id != producerDto.Id){
                return BadRequest();
            }
                
            var existing = _service.GetProducerById(id);
            if (existing == null){ return NotFound(); }

            var producer = producerDto.ToModel();
            _service.UpdateProducer(producer);

            var products = _service.GetAllProducts()
                .Where(p => p.ProducerId == id)
                .ToList();

            foreach (var product in products){
                var productModel = new ProductModel {
                    Id = product.Id,
                    Name = product.Name,
                    ProducerId  = product.ProducerId,
                    ProducerName = producerDto.Name
                };


                _service.UpdateProduct(productModel);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var producer = _service.GetProducerById(id);
            if (producer == null){
                return NotFound();
            }
               

            _service.DeleteProducer(id);
            return NoContent();
        }
    }
} 