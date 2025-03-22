namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SignalPlus.Models;
    using SignalPlus.Models.Enums;
    using SignalPlus.Services.Interfaces;

    public class SignalController : Controller
    {
        private readonly ISignalService _signalService;

        public SignalController(ISignalService signalService)
        {
            _signalService = signalService;
        }

        // Show all signals
        [Route("/allsignals")]
        public async Task<IActionResult> AllSignals(string searchQuery, Status? status, Category? category)
        {
            var signals = await _signalService.GetAllSignalsAsync();

            // Apply filters if values are provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                signals = signals.Where(s => s.Title.Contains(searchQuery) || s.Description.Contains(searchQuery));
            }

            if (status.HasValue)
            {
                signals = signals.Where(s => s.Status == status.Value);
            }

            if (category.HasValue)
            {
                signals = signals.Where(s => s.Category == category.Value);
            }

            return View(signals);
        }

        // Show form to submit a new signal

        [Route("/newSignal")]
        public IActionResult NewSignal()
        {
            return View();
        }

        //// Handle signal submission
        [HttpPost]
        public async Task<IActionResult> NewSignal(Signal signal)
        {
            if (ModelState.IsValid)
            {
                await _signalService.AddSignalAsync(signal);
                return RedirectToAction("AllSignals");
            }
            return RedirectToAction("Index");
        }
    }
}
