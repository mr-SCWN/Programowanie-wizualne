using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using KaliadzichShumer.SneakersShop.RazorPages.Models;
using KaliadzichShumer.SneakersShop.RazorPages.Services;

namespace KaliadzichShumer.SneakersShop.RazorPages.Pages
{
    public class ProducersModel : PageModel
    {
        private readonly ProducerService _producerService;

        public ProducersModel(ProducerService producerService)
        {
            _producerService = producerService;
        }

        public IList<Producer> Producers { get; set; }

        public async Task OnGetAsync()
        {
            Producers = (await _producerService.GetProducersAsync()) as IList<Producer>;
        }
    }
}
