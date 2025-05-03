namespace SignalPlus.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SignalPlus.DTOs;
    using SignalPlus.Models;
    using SignalPlus.Services.Interfaces;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISignalService _signalService;

        public HomeController(ILogger<HomeController> logger, ISignalService signalService)
        {
            _logger = logger;
            _signalService = signalService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var total = await _signalService.GetTotalSignalsCountAsync();
            var completed = await _signalService.GetCompletedSignalsCountAsync();
            
            var homePageDTO = new HomePageDTO
            {
                TotalSignals = total,
                CompletedSignals = completed
            };

            return View(homePageDTO);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
