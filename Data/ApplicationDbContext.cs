using Storyteller.Models;
using Microsoft.EntityFrameworkCore;

namespace Storyteller.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryChat> StoryChats { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }   
        public DbSet<User> Users { get; set; }   
        public DbSet<UserFollowAuthor> UserFollowAuthors { get; set; }   

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentTime = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            baseEntity.CreatedOn = currentTime;
                            baseEntity.UpdatedOn = currentTime;
                            break;
                        case EntityState.Modified:
                            baseEntity.UpdatedOn = currentTime;
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}