﻿namespace SignalPlus.Controllers
{
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

        [HttpGet("/newSignal")]
        public IActionResult NewSignal()
        {
            return View();
        }

        //// Handle signal submission
        [HttpPost("/newSignal")]
        public async Task<IActionResult> NewSignal(NewSignalDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User? user = await _userService.GetCurrentUserAsync(User);

            if (user == null)
            {
                user = await _userService.CreateAnonymousUser(model);
            }

            await _signalService.CreateSignalAsync(user, model);

            return RedirectToAction("AllSignals");
            // TODO: Send email confirmation with tracking number
            // TODO: Save uploaded images, if any
        }

    }
}
