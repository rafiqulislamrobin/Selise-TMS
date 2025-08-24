namespace TMS.Application.features.User.Login
{
    public class LoginUserResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}