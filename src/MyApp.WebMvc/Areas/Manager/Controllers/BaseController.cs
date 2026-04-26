using Microsoft.AspNetCore.Mvc;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BaseController : Controller
    {
        
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        [TempData]
        public string? StatusMessage { get; set; }


        [ViewData]
        public string? ReturnUrl { get; set; }
    }
}
