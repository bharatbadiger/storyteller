using Storyteller.Models;

namespace Storyteller.Services
{
    public interface IStoryChatService
    {
        Task<IEnumerable<StoryChat>> GetAllAsync();
        Task<StoryChat> GetByIdAsync(long id);
        Task<StoryChat> AddAsync(StoryChat storyChat);
        Task<StoryChat> UpdateAsync(StoryChat storyChat);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<StoryChat>> GetByStoryIdAsync(long storyId);
        Task<IEnumerable<StoryChat>> GetByStoryIdOrderByIdAsync(long storyId);
        Task<long?> GetMaxSerialNumberByStoryIdAsync(long storyId);
        
    }
}
