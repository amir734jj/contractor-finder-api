using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.Tests
{
    public class TestExtension
    {
        private readonly EntityDbContext _entityDbContext;

        public TestExtension()
        {
            _entityDbContext = new EntityDbContext(new DbContextOptionsBuilder<EntityDbContext>()
                .UseSqlite("Data Source=:memory:")
                .Options);
        }

        [Fact]
        public void Test1()
        {
            var q = _entityDbContext.TestModels
                .Select(x => x)
                .ToSql();

            Console.WriteLine(q);
        }
    }
}