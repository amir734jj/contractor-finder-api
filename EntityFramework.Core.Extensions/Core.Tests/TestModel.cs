using System.ComponentModel.DataAnnotations;

namespace Core.Tests
{
    public class TestModel
    {
        [Key]
        public string Id { get; set; }
    }
}