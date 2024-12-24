using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OidcServer.Helpers;
using OidcServer.Models;
using OidcServer.Repositories;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace OidcServer.Controllers
{
    public class AuthorizeController : Controller
    {
        public readonly IUserRepository userRepository;
        public readonly ICodeItemRepository codeRepository;

        public AuthorizeController(IUserRepository userRepository, ICodeItemRepository codeRepository = null)
        {
            this.userRepository = userRepository;
            this.codeRepository = codeRepository;
        }

        public IActionResult Index(AuthenticationRequestModel authenticationRequest)
        {
            return View(authenticationRequest);
        }

        [HttpPost]
        public IActionResult Authorize(AuthenticationRequestModel authenticationRequest, string user, string[] scopes)
        {
            if (userRepository.FindByUsername(user) == null)
            {
                return View("UserNotFound");
            }

            string code = GeneratedCode();

            var model = new CodeFlowResponseViewModel()
            {
                Code = code,
                State = authenticationRequest.State,
                RedirectUri = authenticationRequest.RedirectUri
            };

            codeRepository.Add(code, new CodeItem() { 
                AuthenticationRequest = authenticationRequest,
                User = user,
                Scopes = scopes
            });

            return View("SubmitForm", model);
        }

        [Route("oauth/token")]
        [HttpPost]
        public IActionResult ReturnTokens(string grant_type, string code, string redirect_uri)
        {
            if (grant_type != "authorization_code")
            {
                return BadRequest();
            }

            var codeItem = codeRepository.FindByCode(code);
            if (codeItem == null)
            {
                return BadRequest();
            }

            codeRepository.Delete(code);

            if (codeItem.AuthenticationRequest.RedirectUri != redirect_uri)
            {
                return BadRequest();
            }

            var jwk = JwkLoader.LoadFromDefault();

            var model = new AuthenticationResponseModel()
            {
                AccessToken = GenerateAccessToken(codeItem.User, string.Join(' ', codeItem.Scopes), codeItem.AuthenticationRequest.ClientId, codeItem.AuthenticationRequest.Nonce, jwk),
                TokenType = "Bearer",
                ExpiresIn = 3600,
                RefreshToken = GeneratedRefreshToken(),
                IdToken = GenerateIdToken(codeItem.User, codeItem.AuthenticationRequest.ClientId, codeItem.AuthenticationRequest.Nonce, jwk)
            };


            return Json(model);
        }

        private static string GeneratedRefreshToken()
        {
            return GeneratedCode();
        }

        static Random random = new();
        private static string GeneratedCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 32)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateAccessToken(string userId, string scope, string audience, string nonce, JsonWebKey jsonWebKey)
        {
            // access_token can be the same as id_token, but here we might have different values for expirySeconds so we use 2 different functions

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new("scope", scope) 
            };
            var idToken = JwtGenerator.GenerateJWTToken(
                20 * 60,
                "https://localhost:7082/",
                audience,
                nonce,
                claims,
                jsonWebKey
                );

            return idToken;
        }

        private string GenerateIdToken(string userId, string audience, string nonce, JsonWebKey jsonWebKey)
        {
            // https://openid.net/specs/openid-connect-core-1_0.html#IDToken
            // we can return some claims defined here: https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId)
            };

            var idToken = JwtGenerator.GenerateJWTToken(
                20 * 60,
                "https://localhost:7082/",
                audience,
                nonce,
                claims,
                jsonWebKey
                );


            return idToken;
        }
    }
}
