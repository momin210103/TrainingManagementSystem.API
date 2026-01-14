using TMS.Application.Auth.DTOs;
using TMS.Application.Auth.Interfaces;
using TMS.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.Options;
using TMS.Application.Common.Settings;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace TMS.Application.Auth.Services
{
    public class AuthService: IAuthService
    {
        private readonly ITmsDbContext _context;
        private readonly JwtSettings _jwt;

        public AuthService(ITmsDbContext context, IOptions<JwtSettings> jwtOptions)
        {
            _context = context;
            _jwt = jwtOptions.Value;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (employee == null)
            {
                throw new UnauthorizedAccessException("Invalid creadentials");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,employee.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,employee.Email),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
                signingCredentials:creds
                );
            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireAt = token.ValidTo
            };


            
        }
    }
}
