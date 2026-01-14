namespace TMS.Application.Auth.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
    }
}
