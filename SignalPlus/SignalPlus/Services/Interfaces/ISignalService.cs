namespace SignalPlus.Services.Interfaces
{
    using SignalPlus.Models.Enums;
    using SignalPlus.Models;
    using SignalPlus.DTOs.Signal;

    public interface ISignalService
    {
        Task<IEnumerable<Signal>> GetAllSignalsAsync();
        
        Task<IEnumerable<Signal>> GetSignalsByStatusAsync(Status status);

        Task<IEnumerable<Signal>> GetSignalsByUserIdAsync(string userId);

        Task<IEnumerable<Signal>> GetSignalsByCategoryAsync(Category category);
        
        Task<IEnumerable<Signal>> GetLastThreeSignalsAsync();
        
        Task<int> GetTotalSignalsCountAsync();
        
        Task<int> GetCompletedSignalsCountAsync();

        Task<Signal> CreateSignalAsync(User user, NewSignalDTO model);
    }

}
