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
            var inProgressSignals = await _signalService.GetInProgressSignalsCountAsync();
            var completedSignals = await _signalService.GetCompletedSignalsCountAsync();

            ViewData["TotalSignals"] = totalSignals;
            ViewData["InProgressSignals"] = inProgressSignals;
            ViewData["CompletedSignals"] = completedSignals;

            return View();
        }
    }
}
