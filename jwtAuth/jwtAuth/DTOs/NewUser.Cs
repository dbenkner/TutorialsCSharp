using System.ComponentModel.DataAnnotations;

namespace jwtAuth.DTOs
{
    public class NewUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Length(8,32)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
    }
}
