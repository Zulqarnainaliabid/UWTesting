namespace Upwork.Testing.Data.DTOs.Auth
{
    public class UserForgotPasswordDto
    {
        public string Email { get; set; }
    }
    public class ResetPasswordResource
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
