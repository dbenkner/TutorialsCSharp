using System.ComponentModel.DataAnnotations;

namespace jwtAuth.Models
{
    public class Role
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? RoleName { get; set; }    
    }
}
