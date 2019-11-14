using Models.Enums;

namespace Models.ViewModels
{
    /// <summary>
    ///     Register view model
    /// </summary>
    public class RegisterViewModel
    {
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Email { get; set; }
        
        public RoleEnum Role { get; set; }
    }
}