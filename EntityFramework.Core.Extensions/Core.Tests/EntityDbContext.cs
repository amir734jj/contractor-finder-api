using Microsoft.EntityFrameworkCore;

namespace Core.Tests
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> optionsBuilderOptions) : base(optionsBuilderOptions) { }

        public DbSet<TestModel> TestModels { get; set; }
    }
}