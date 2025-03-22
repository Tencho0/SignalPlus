namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SignalPlus.Services.Interfaces;

    public class AboutController : Controller
    {
        private readonly ISignalService _signalService;

        public AboutController(ISignalService signalService)
        {
            _signalService = signalService;
        }

        public async Task<IActionResult> Index()
        {
            var totalSignals = await _signalService.GetTotalSignalsCountAsync();
            var completedSignals = await _signalService.GetCompletedSignalsCountAsync();

            ViewData["TotalSignals"] = totalSignals;
            ViewData["CompletedSignals"] = completedSignals;

            return View();
        }
    }
}
