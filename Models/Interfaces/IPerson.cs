using Microsoft.AspNetCore.Identity;

namespace Models.Interfaces
{
    public interface IPerson
    {
        [ProtectedPersonalData]
        string Email { get; set; }
        
        [ProtectedPersonalData]
        string PhoneNumber { get; set; }
        
        [ProtectedPersonalData]
        string Firstname { get; set; }
        
        [ProtectedPersonalData]
        string Lastname { get; set; }
    }
}