using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OidcServer.Helpers
{
    public class JwtGenerator
    {
        public static string GenerateJWTToken(int expirySeconds, string issuer, string audience, string nonce, IEnumerable<Claim> claims, JsonWebKey jsonWebKey)
        {
            var signingCredentials = new SigningCredentials(jsonWebKey, SecurityAlgorithms.RsaSha256);

            var additionalClaims = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Nonce, nonce }
            };

            var jwtHeader = new JwtHeader(signingCredentials);
            var jwtPayload = new JwtPayload(issuer, audience, claims, additionalClaims, null, DateTime.UtcNow.AddSeconds(expirySeconds), DateTime.UtcNow);

            var token = new JwtSecurityToken(
                jwtHeader, jwtPayload
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
