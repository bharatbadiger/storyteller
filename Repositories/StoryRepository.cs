using Microsoft.EntityFrameworkCore;
using Storyteller.Data;
using Storyteller.Models;

namespace Storyteller.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStoryChatRepository _storyChatRepository;
        public StoryRepository(ApplicationDbContext context, IStoryChatRepository storyChatRepository)
        {
            _context = context;
            _storyChatRepository = storyChatRepository;
        }

        public async Task<IEnumerable<Story>> GetAllAsync()
        {
            return await _context.Stories
                                 .Include(s => s.Category)
                                 .Include(s => s.Author)
                                 .ToListAsync();
        }

        public async Task<Story> GetByIdAsync(long id)
        {
            return await _context.Stories
                                 .Include(s => s.Category)
                                 .Include(s => s.Author)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Story> AddAsync(Story story)
        {
            _context.Stories.Add(story);
            await _context.SaveChangesAsync();
            return story;
        }

        public async Task<Story> UpdateAsync(Story story)
        {
            _context.Entry(story).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return story;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return false;
            }
            var storyChats = await _storyChatRepository.GetByStoryIdAsync(id);
            foreach (var storyChat in storyChats)
            {
                await _storyChatRepository.DeleteAsync(storyChat.Id);
            }
            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Story>> GetByNameAsync(string name)
        {
            return await _context.Stories
                                 .Where(s => s.Name.Contains(name))
                                 .Include(s => s.Category)
                                 .Include(s => s.Author)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Story>> GetByNameContainingAsync(string name)
        {
            return await _context.Stories
                                 .Where(a => a.Name.Contains(name))
                                 .Include(s => s.Category)
                                 .Include(s => s.Author)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Story>> GetByAuthorContainingAsync(string authorName)
        {
            return await _context.Stories
                .Include(s => s.Author)
                .Include(s => s.Category)
                .Where(s => s.Author.Name.Contains(authorName))
                .ToListAsync();
        }
        public async Task<IEnumerable<Story>> GetByTagsContainingAsync(string tag)
        {
            return await _context.Stories
            .Include(s => s.Author)
            .Include(s => s.Category)
            .Where(s => s.Tags.Contains(tag))
            .ToListAsync();
        }
    }
}
