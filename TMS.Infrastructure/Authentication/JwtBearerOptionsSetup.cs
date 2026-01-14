using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TMS.Application.Common.Settings;

namespace TMS.Infrastructure.Authentication
{
    public sealed class JwtBearerOptionsSetup: IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtSettings _jwt;

        public JwtBearerOptionsSetup(IOptions<JwtSettings> jwt)
        {
            _jwt = jwt.Value;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            if (name != JwtBearerDefaults.AuthenticationScheme)
                return;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _jwt.Issuer,
                ValidAudience = _jwt.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwt.Key)),
                RoleClaimType = ClaimTypes.Role
            };
        }

        public void Configure(JwtBearerOptions options)
        => Configure(JwtBearerDefaults.AuthenticationScheme, options);
    }
}
