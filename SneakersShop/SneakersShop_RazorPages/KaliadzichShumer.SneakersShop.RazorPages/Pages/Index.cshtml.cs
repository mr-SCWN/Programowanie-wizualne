using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using KaliadzichShumer.SneakersShop.RazorPages.Models;
using KaliadzichShumer.SneakersShop.RazorPages.Services;

namespace KaliadzichShumer.SneakersShop.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;
        private readonly ProducerService _producerService;

        public IndexModel(ProductService productService, ProducerService producerService)
        {
            _productService = productService;
            _producerService = producerService;
        }

        public IList<Product> Products { get; set; }
        public IList<Producer> Producers { get; set; }

        public async Task OnGetAsync()
        {
            Products = (await _productService.GetProductsAsync()) as IList<Product>;
            Producers = (await _producerService.GetProducersAsync()) as IList<Producer>;
        }
    }
}
