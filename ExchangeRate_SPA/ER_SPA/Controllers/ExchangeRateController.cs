using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ER_SPA.Controllers
{
    [Route("api/[controller]")]
    public class ExchangeRateController : Controller
    {
        private readonly IExchangeRateService exchangeRateService;
        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            this.exchangeRateService = exchangeRateService;
        }

        // GET api/<controller>/5
        [HttpGet("{date}")]
        public IActionResult Get(string date)
        {
            return exchangeRateService.Get(date);
        }
    }
}
