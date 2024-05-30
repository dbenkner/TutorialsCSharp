using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace jwtAuth.Models
{
    [Index("Username", IsUnique = true)]
    public class User
    {
        public int Id { get; set; } = 0;
        [StringLength(30)]
        public string Username { get; set; } = string.Empty;
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;
        [StringLength(60)]
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        [StringLength(255)]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        [JsonIgnore]
        [StringLength(255)]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public virtual ICollection<UserToRole> Roles { get; set; }


        public User() { }
    }
}
