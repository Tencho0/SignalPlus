namespace SignalPlus.Services
{
    using SignalPlus.Data;
    using SignalPlus.Models.Enums;
    using SignalPlus.Models;
    using SignalPlus.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

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

        // Get signals by category
        public async Task<IEnumerable<Signal>> GetSignalsByCategoryAsync(Category category)
        {
            return await _context.Signals.Where(s => s.Category == category).ToListAsync();
        }

        // Get last 3 signals
        public async Task<IEnumerable<Signal>> GetLastThreeSignalsAsync()
        {
            return await _context.Signals.OrderByDescending(s => s.CreatedAt).Take(3).ToListAsync();
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

        // Add a new signal
        public async Task AddSignalAsync(Signal signal)
        {
            _context.Signals.Add(signal);
            await _context.SaveChangesAsync();
        }
    }

}
