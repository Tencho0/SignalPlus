﻿namespace SignalPlus.Services
{
    using SignalPlus.Data;
    using SignalPlus.Models.Enums;
    using SignalPlus.Models;
    using SignalPlus.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using SignalPlus.DTOs.Signal;

    public class SignalService : ISignalService
    {
        private readonly ApplicationDbContext _context;

        public SignalService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all signals
        public async Task<IEnumerable<Signal>> GetAllSignalsAsync()
        {
            return await _context.Signals.ToListAsync();
        }

        // Get signals by status
        public async Task<IEnumerable<Signal>> GetSignalsByStatusAsync(Status status)
        {
            return await _context.Signals.Where(s => s.Status == status).ToListAsync();
        }

        // Get signals by userId
        public async Task<IEnumerable<Signal>> GetSignalsByUserIdAsync(string userId)
        {
            return await _context.Signals.Where(s => s.UserId == userId).OrderByDescending(s => s.CreatedAt).ToListAsync();
        }

        // Get signals by category
        public async Task<IEnumerable<Signal>> GetSignalsByCategoryAsync(Category category)
        {
            return await _context.Signals.Where(s => s.Category == category && s.IsApproved == true).ToListAsync();
        }

        // Get last 3 signals
        public async Task<IEnumerable<Signal>> GetLastThreeSignalsAsync()
        {
            return await _context.Signals.Where(s => s.IsApproved == true).OrderByDescending(s => s.CreatedAt).Take(3).ToListAsync();
        }

        // Get total signals count
        public async Task<int> GetTotalSignalsCountAsync()
        {
            return await _context.Signals.CountAsync();
        }

        // Get completed signals count
        public async Task<int> GetCompletedSignalsCountAsync()
        {
            return await _context.Signals.CountAsync(s => s.Status == Status.Приключен);
        }

        public async Task<Signal> CreateSignalAsync(User user, NewSignalDTO model)
        {
            var signal = new Signal
            {
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                Status = Status.Регистриран,
                CreatedAt = DateTime.UtcNow,
                LocationAddress = model.Address,
                Latitude = 0, // TODO: Add from map or geolocation if available
                Longitude = 0, // TODO: Same as above
                UserId = user.Id,
                User = user
            };
            user.Signals.Add(signal);
            _context.Signals.Add(signal);

            // Optional: Handle image upload here later if you store images
            // Optional: Generate tracking number and assign it to signal

            await _context.SaveChangesAsync();

            return signal;
        }

        public async Task<Signal?> GetByIdAsync(int id)
        {
            var signal = await _context.Signals
                // .Include(s => s.Photos) // assuming Signal has a collection of Photo entities
                .FirstOrDefaultAsync(s => s.Id == id);

            //TODO: Add DTO for this purpose
            return signal;
        }

        public async Task<bool> ApproveAsync(int id)
        {
            var signal = await _context.Signals.FindAsync(id);
            if (signal == null) return false;

            signal.IsApproved = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeclineAsync(int id)
        {
            var signal = await _context.Signals.FindAsync(id);
            if (signal == null) return false;

            signal.IsApproved = false;
            await _context.SaveChangesAsync();

            return true;
        }

    }

}
