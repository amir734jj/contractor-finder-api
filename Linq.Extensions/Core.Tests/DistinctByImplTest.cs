using System;
using System.Linq;
using AutoFixture;
using Linq.Extensions;
using Xunit;

namespace Core.Tests
{
    public class DistinctByImplTest
    {
        private readonly IFixture _fixture;

        public DistinctByImplTest()
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
            var distinct = models.DistinctBy(x => x.Prop1).ToList();

            // Assert
            Assert.Equal(models, distinct);
        }

        [Fact]
        public void Test__DistinctBy2()
        {
            // Arrange
            var prop2Val = _fixture.Create<string>();
            
            var models = _fixture.Build<DummyModel>()
                .Without(x => x.Prop1)
                .With(x => x.Prop2, prop2Val)
                .Without(x => x.Prop3)
                .CreateMany()
                .ToList();

            // Act
            var distinct = models.DistinctBy(x => x.Prop1, x => x.Prop2).ToList();

            // Assert
            Assert.Equal(models.Take(1), distinct);
        }
    }
}