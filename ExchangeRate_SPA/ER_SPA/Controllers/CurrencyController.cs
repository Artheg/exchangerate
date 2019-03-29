using ER_SPA.Services;
using Microsoft.AspNetCore.Mvc;

namespace ER_SPA.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [HttpGet("{date}")]
        public IActionResult Get(string date)
        {
            return currencyService.Get(date);
        }
    }
}
