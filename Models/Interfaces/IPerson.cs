namespace Models.Interfaces
{
    public interface IPerson : IBasicModel
    {
        string Email { get; set; }
        
        string Phone { get; set; }
        
        string Fullname { get; set; }
    }
}