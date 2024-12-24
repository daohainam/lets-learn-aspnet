namespace OidcServer.Models
{
    public class CodeItem
    {
        public required AuthenticationRequestModel AuthenticationRequest { get; set; }
        public required string User { get; set; }
        public required string[] Scopes { get; set; }
    }
}
