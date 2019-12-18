using System.Linq;
using AutoFixture;
using Linq.Extensions;
using Xunit;

namespace Core.Tests
{
    public class DistinctByExceptTest
    {
        private readonly IFixture _fixture;

        public DistinctByExceptTest()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void Test__DistinctBy1()
        {
            // Arrange
            var models = _fixture.Build<DummyModel>()
                .With(x => x.Prop1)
                .Without(x => x.Prop2)
                .Without(x => x.Prop3)
                .CreateMany()
                .ToList();

            // Act
            var distinct = models.DistinctByExcept(x => x.Prop2, x => x.Prop3).ToList();

            // Assert
            Assert.Equal(models, distinct);
        }
        
        [Fact]
        public void Test__DistinctBy2()
        {
            // Arrange
            var models = _fixture.Build<DummyModel>()
                .With(x => x.Prop1)
                .Without(x => x.Prop2)
                .Without(x => x.Prop3)
                .CreateMany()
                .ToList();

            // Act
            var distinct = models.DistinctByExcept(x => x.Prop1).ToList();

            // Assert
            Assert.Equal(models.Take(1), distinct);
        }
    }
}