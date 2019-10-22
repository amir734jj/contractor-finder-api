using System.ComponentModel.DataAnnotations;

namespace Models.Interfaces
{
    public interface IBasicModel
    {
        [Key]
        int Id { get; set; }
    }
}