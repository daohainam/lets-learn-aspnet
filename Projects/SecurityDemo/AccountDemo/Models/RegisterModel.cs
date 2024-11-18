namespace SignInSignOutDemo.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? Password { get; set; }
    }
}
