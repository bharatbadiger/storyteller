using Storyteller.Models;
using Storyteller.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storyteller.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _subscriptionRepository.GetAllAsync();
        }

        public async Task<Subscription> GetByIdAsync(long id)
        {
            return await _subscriptionRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Subscription>> GetByNameAsync(string name)
        {
            return await _subscriptionRepository.GetByNameContainingAsync(name);
        }        

        public async Task<Subscription> AddAsync(Subscription subscription)
        {
            var existingSubscription = await _subscriptionRepository.GetAllAsync();
            if (existingSubscription.Any(s => s.Title == subscription.Title))
            {
                throw new Exception("Subscription already exists");
            }
            return await _subscriptionRepository.AddAsync(subscription);
        }

        public async Task<Subscription> UpdateAsync(Subscription subscription)
        {
            return await _subscriptionRepository.UpdateAsync(subscription);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _subscriptionRepository.DeleteAsync(id);
        }

    }
}
