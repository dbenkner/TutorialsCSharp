namespace jwtAuth.DTOs
{
    public class ResetPwDTO
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public int userId { get; set; }
    }
}
