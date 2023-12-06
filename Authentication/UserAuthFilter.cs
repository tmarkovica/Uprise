using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Uprise.Authentication;

public class UserAuthFilter: Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            context.Result = new UnauthorizedObjectResult("Authorization Header is not present");
            return;
        }

        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>(AuthConstants.JWT_KEY)));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidIssuer = configuration.GetValue<string>(AuthConstants.JWT_ISSUER_KEY),
                ValidateAudience = true,
                ValidAudience = configuration.GetValue<string>(AuthConstants.JWT_AUDIENCE_KEY),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5) // No tolerance for the expiration date
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch (Exception e)
        {
            context.Result = new UnauthorizedObjectResult(e.Message);
        }
    }
}
