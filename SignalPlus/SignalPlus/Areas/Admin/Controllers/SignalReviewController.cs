namespace SignalPlus.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SignalPlus.Models.Enums;
    using SignalPlus.Services.Interfaces;

    [Area("Admin")]
    [Authorize(Roles = Constants.AdministratorRoleName)]
    public class SignalReviewController : Controller
    {
        private readonly ISignalService _signalService;

        public SignalReviewController(ISignalService signalService)
        {
            _signalService = signalService;
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

            return View(signals.OrderByDescending(s => s.CreatedAt));
        }
    }
}
