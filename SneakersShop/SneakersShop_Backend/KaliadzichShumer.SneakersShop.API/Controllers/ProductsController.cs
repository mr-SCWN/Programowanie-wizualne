using Microsoft.AspNetCore.Mvc;
using System.Linq;

using KaliadzichShumer.SneakersShop.INTERFACES.Services;
using KaliadzichShumer.SneakersShop.API.Models;

namespace KaliadzichShumer.SneakersShop.API.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase{
        private readonly ISneakersShopService _service;

        public ProductsController(ISneakersShopService service){
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var products = _service.GetAllProducts();
            return Ok(products.Select(p => p.ToDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var product = _service.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product.ToDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDto productDto){
            var product = productDto.ToModel();
            var created = _service.CreateProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDto productDto) {
            if (id != productDto.Id){return BadRequest();}
                
            var existing = _service.GetProductById(id);
            if (existing == null){
                return NotFound();
            }
               
            var product = productDto.ToModel();
            _service.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null){
                return NotFound();
            }
            
            _service.DeleteProduct(id);
            return NoContent();
        }
    }
} 