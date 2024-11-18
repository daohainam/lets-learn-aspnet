namespace SignInSignOutDemo.Models
{
    public class UserProfileModel
    {
        public string UserName { get; set; } = string.Empty;
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? Password { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}
