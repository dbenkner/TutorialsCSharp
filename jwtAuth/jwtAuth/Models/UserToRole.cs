using System.Text.Json.Serialization;

namespace jwtAuth.Models
{
    public class UserToRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
