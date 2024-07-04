using Storyteller.Models;

namespace Storyteller.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription> GetByIdAsync(long id);
        Task<IEnumerable<Subscription>> GetByNameAsync(string name);
        Task<Subscription> AddAsync(Subscription subscription);
        Task<Subscription> UpdateAsync(Subscription subscription);
        Task<bool> DeleteAsync(long id);
    }
}
