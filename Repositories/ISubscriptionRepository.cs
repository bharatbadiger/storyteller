using Storyteller.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storyteller.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription> GetByIdAsync(long id);
        Task<IEnumerable<Subscription>> GetByNameContainingAsync(string name);
        Task<Subscription> AddAsync(Subscription subscription);
        Task<Subscription> UpdateAsync(Subscription subscription);
        Task<bool> DeleteAsync(long id);
    }
}
