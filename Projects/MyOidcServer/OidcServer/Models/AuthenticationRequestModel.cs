using Microsoft.AspNetCore.Mvc;

namespace OidcServer.Models
{
    public class AuthenticationRequestModel
    {
        [BindProperty(Name = "client_id", SupportsGet = true)]
        public string ClientId { get; set; } = string.Empty;
        [BindProperty(Name = "redirect_uri", SupportsGet = true)]
        public string RedirectUri { get; set; } = string.Empty;
        [BindProperty(Name = "response_type", SupportsGet = true)]
        public string ResponseType { get; set; } = string.Empty;
        [BindProperty(Name = "scope", SupportsGet = true)]
        public string Scope { get; set; } = string.Empty;
        [BindProperty(Name = "code_challenge", SupportsGet = true)]
        public string CodeChallenge { get; set; } = string.Empty;
        [BindProperty(Name = "code_challenge_method", SupportsGet = true)]
        public string CodeChallengeMethod { get; set; } = string.Empty;
        [BindProperty(Name = "response_mode", SupportsGet = true)]
        public string ResponseMode { get; set; } = string.Empty;
        [BindProperty(Name = "nonce", SupportsGet = true)]
        public string Nonce { get; set; } = string.Empty;
        [BindProperty(Name = "state", SupportsGet = true)]
        public string State { get; set; } = string.Empty;
    }
}
