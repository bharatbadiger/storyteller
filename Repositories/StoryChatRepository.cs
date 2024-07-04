using Microsoft.EntityFrameworkCore;
using Storyteller.Data;
using Storyteller.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storyteller.Repositories
{
    public class StoryChatRepository : IStoryChatRepository
    {
        private readonly ApplicationDbContext _context;

        public StoryChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoryChat>> GetAllAsync()
        {
            return await _context.StoryChats
                                 .Include(sc => sc.Story)
                                 .ToListAsync();
        }

        public async Task<StoryChat> GetByIdAsync(long id)
        {
            return await _context.StoryChats
                                 .Include(sc => sc.Story)
                                 .FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<StoryChat> AddAsync(StoryChat storyChat)
        {
            _context.StoryChats.Add(storyChat);
            await _context.SaveChangesAsync();
            return storyChat;
        }

        public async Task<StoryChat> UpdateAsync(StoryChat storyChat)
        {
            _context.Entry(storyChat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return storyChat;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var storyChat = await _context.StoryChats.FindAsync(id);
            if (storyChat == null)
            {
                return false;
            }

            _context.StoryChats.Remove(storyChat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StoryChat>> GetByStoryIdAsync(long storyId)
        {
            return await _context.StoryChats
                                 .Where(sc => sc.Story.Id == storyId)
                                 .ToListAsync();
        }

        public async Task<long?> GetMaxSerialNumberByStoryIdAsync(long storyId)
        {
            return await _context.StoryChats
                                 .Where(s => s.Story.Id == storyId)
                                 .MaxAsync(s => (long?)s.SerialNumber);
        }

        public async Task<IEnumerable<StoryChat>> GetByStoryIdOrderBySerialNumberAsync(long storyId)
        {
            return await _context.StoryChats
                                 .Where(s => s.Story.Id == storyId)
                                .Include(s => s.Story)
                                    .ThenInclude(story => story.Author)
                                .Include(s => s.Story)
                                    .ThenInclude(story => story.Category)
                                 .OrderByDescending(s => s.Id)
                                 .ToListAsync();
        }
    }
}
