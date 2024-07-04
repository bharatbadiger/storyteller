using Storyteller.Models;
using Storyteller.Repositories;

namespace Storyteller.Services
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;

        public StoryService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<IEnumerable<Story>> GetAllAsync()
        {
            return await _storyRepository.GetAllAsync();
        }

        public async Task<Story> GetByIdAsync(long id)
        {
            return await _storyRepository.GetByIdAsync(id);
        }

        public async Task<Story> AddAsync(Story story)
        {
            var existingStory = await _storyRepository.GetByNameAsync(story.Name);
            if (existingStory.Any())
            {
                throw new Exception("Story already exists");
            }
            return await _storyRepository.AddAsync(story);
        }

        public async Task<Story> UpdateAsync(Story story)
        {
            return await _storyRepository.UpdateAsync(story);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _storyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Story>> GetByNameAsync(string name)
        {
            return await _storyRepository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Story>> GetByNameContainingAsync(string name)
        {
            return await _storyRepository.GetByNameContainingAsync(name);
        }

        public async Task<IEnumerable<Story>> GetByAuthorContainingAsync(string name)
        {
            return await _storyRepository.GetByAuthorContainingAsync(name);
        }

        public async Task<IEnumerable<Story>> GetByTagsContainingAsync(string name)
        {
            return await _storyRepository.GetByTagsContainingAsync(name);
        }
    }
}
