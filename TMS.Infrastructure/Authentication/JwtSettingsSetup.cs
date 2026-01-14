using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TMS.Application.Common.Settings;

namespace TMS.Infrastructure.Authentication
{
    public sealed class JwtSettingsSetup: IConfigureOptions<JwtSettings>
    {
        private readonly IConfiguration _configuration;

        public JwtSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtSettings options)
        {
            _configuration.GetSection("Jwt").Bind(options);
        }
    }
}
