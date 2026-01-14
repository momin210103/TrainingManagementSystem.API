using TMS.Application.Auth.DTOs;

namespace TMS.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
