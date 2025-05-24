namespace SignalPlus.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SignalPlus.Models.Enums;
    using SignalPlus.Services.Interfaces;

    [Area("Admin")]
    [Authorize(Roles = Constants.AdministratorRoleName)]
    public class SignalController : Controller
    {
        private readonly ISignalService _signalService;

        public SignalController(ISignalService signalService)
        {
            _signalService = signalService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var signal = await _signalService.GetByIdAsync(id);
            if (signal == null) return NotFound();

            return View(signal);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await _signalService.ApproveAsync(id);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Decline(int id)
        {
            await _signalService.DeclineAsync(id);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var signal = await _signalService.GetByIdAsync(id);
            if (signal == null)
            {
                return NotFound();
            }

            if (signal.Status == Status.ВОбработка && signal.IsApproved == true)
            {
                await _signalService.SetStatusAsync(signal.Id, Status.Приключен);
            }

            return RedirectToAction("Index", new { section = "InProgress" });
        }

        [HttpPost]
        public async Task<IActionResult> StartProcessing(int id)
        {
            await _signalService.SetStatusAsync(id, Status.ВОбработка);
            return RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> Index(string searchQuery, Status? status, Category? category, string? isApproved)
        {
            var signals = await _signalService.GetAllSignalsAsync();

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

            if (!string.IsNullOrEmpty(isApproved))
            {
                if (isApproved == "За Проверка")
                {
                    signals = signals.Where(s => s.IsApproved == null);
                }
                else
                {
                    signals = signals.Where(s => s.IsApproved == false);
                }
            }

            //return View(signals.OrderByDescending(s => s.CreatedAt));
            return PartialView("Index", signals.OrderByDescending(s => s.CreatedAt));
        }

        public async Task<IActionResult> Statistics()
        {
            var allSignals = await _signalService.GetAllSignalsAsync();
            ViewBag.Total = allSignals.Count();
            ViewBag.Approved = allSignals.Count(s => s.IsApproved == true);
            ViewBag.Rejected = allSignals.Count(s => s.IsApproved == false);
            ViewBag.Completed = allSignals.Count(s => s.Status == Status.Приключен);

            return PartialView("_Statistics", allSignals);
        }

        public async Task<IActionResult> ForApproval()
        {
            var signals = await _signalService.GetAllSignalsAsync();
            var filtered = signals.Where(s => s.IsApproved == null);

            return PartialView("_ForApproval", filtered.OrderByDescending(s => s.CreatedAt));
        }

        public async Task<IActionResult> ToBeStarted()
        {
            var signals = await _signalService.GetAllSignalsAsync();
            var approvedNotStarted = signals.Where(s => s.IsApproved == true && s.Status == Status.Регистриран);

            return PartialView("_ToBeStarted", approvedNotStarted.OrderByDescending(s => s.CreatedAt));
        }

        public async Task<IActionResult> InProgress()
        {
            var signals = await _signalService.GetAllSignalsAsync();
            var filtered = signals.Where(s => s.Status == Status.ВОбработка);

            return PartialView("_InProgress", filtered.OrderByDescending(s => s.CreatedAt));
        }

        public async Task<IActionResult> Completed()
        {
            var signals = await _signalService.GetAllSignalsAsync();
            var completedSignals = signals.Where(s => s.Status == Status.Приключен);

            return PartialView("_Completed", completedSignals.OrderByDescending(s => s.CreatedAt));
        }

        public async Task<IActionResult> Rejected()
        {
            var signals = await _signalService.GetAllSignalsAsync();
            var rejectedSignals = signals.Where(s => s.IsApproved == false);

            return PartialView("_Rejected", rejectedSignals.OrderByDescending(s => s.CreatedAt));
        }
    }

}
