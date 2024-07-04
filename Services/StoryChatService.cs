using Storyteller.Models;
using Storyteller.Repositories;

namespace Storyteller.Services
{
    public class StoryChatService : IStoryChatService
    {
        private readonly IStoryChatRepository _storyChatRepository;

        public StoryChatService(IStoryChatRepository storyChatRepository)
        {
            _storyChatRepository = storyChatRepository;
        }

        public async Task<IEnumerable<StoryChat>> GetAllAsync()
        {
            return await _storyChatRepository.GetAllAsync();
        }

        public async Task<StoryChat> GetByIdAsync(long id)
        {
            return await _storyChatRepository.GetByIdAsync(id);
        }

        public async Task<StoryChat> AddAsync(StoryChat storyChat)
        {
            return await _storyChatRepository.AddAsync(storyChat);
        }

        public async Task<StoryChat> UpdateAsync(StoryChat storyChat)
        {
            return await _storyChatRepository.UpdateAsync(storyChat);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _storyChatRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<StoryChat>> GetByStoryIdAsync(long storyId)
        {
            return await _storyChatRepository.GetByStoryIdAsync(storyId);
        }

        public async Task<IEnumerable<StoryChat>> GetByStoryIdOrderByIdAsync(long storyId)
        {
            return await _storyChatRepository.GetByStoryIdOrderByIdAsync(storyId);
        }
        public async Task<long?> GetMaxSerialNumberByStoryIdAsync(long storyId)
        {
            return await _storyChatRepository.GetMaxSerialNumberByStoryIdAsync(storyId);
        }
    }
}
