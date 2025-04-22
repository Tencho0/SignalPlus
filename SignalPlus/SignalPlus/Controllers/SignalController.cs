namespace SignalPlus.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.DTOs.Signal;
    using SignalPlus.Models;
    using SignalPlus.Models.Enums;
    using SignalPlus.Services;
    using SignalPlus.Services.Interfaces;

    public class SignalController : Controller
    {
        private readonly ISignalService _signalService;
        private readonly IUserService _userService;

        public SignalController(ISignalService signalService, IUserService userService)
        {
            _signalService = signalService;
            _userService = userService;
        }

        // Show all signals
        [Route("/allsignals")]
        public async Task<IActionResult> AllSignals(string searchQuery, Status? status, Category? category, int page = 1, int pageSize = 9)
        {
            var signals = await _signalService.GetAllSignalsAsync();
            signals = signals.Where(x => x.IsApproved == true);
            
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
            
             // Pagination
            var totalItems = signals.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedSignals = signals
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.Status = status;
            ViewBag.Category = category;

            return View(pagedSignals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var signal = await _signalService.GetByIdAsync(id);
            if (signal == null)
                return NotFound();

            return View(signal);
        }

        [HttpGet("/newSignal")]
        public IActionResult NewSignal()
        {
            return View();
        }

        //// Handle signal submission
        [HttpPost("/newSignal")]
        public async Task<IActionResult> NewSignal(NewSignalDTO model, [FromServices] IReCaptchaService captchaService)
        {
            var recaptchaToken = Request.Form["g-recaptcha-response"];
            var isCaptchaValid = await captchaService.VerifyToken(recaptchaToken);

            if (!isCaptchaValid)
            {
                ModelState.AddModelError(string.Empty, "Моля, потвърдете, че не сте робот.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User? user = await _userService.GetCurrentUserAsync(User);

            await _signalService.CreateSignalAsync(user, model, model.Images);

            return RedirectToAction("AllSignals");
            // TODO: Send email confirmation with tracking number
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdministratorRoleName)]
        public async Task<IActionResult> Approve(int id)
        {
            var success = await _signalService.ApproveAsync(id);
            if (!success) return NotFound();

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdministratorRoleName)]
        public async Task<IActionResult> Decline(int id)
        {
            var success = await _signalService.DeclineAsync(id);
            if (!success) return NotFound();

            return RedirectToAction("Details", new { id });
        }
    }
}
