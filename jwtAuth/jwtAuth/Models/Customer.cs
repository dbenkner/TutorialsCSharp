using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jwtAuth.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [StringLength(40)]
        [EmailAddress(ErrorMessage="Invalid Email Address")]
        public string? Email { get; set; }
        [StringLength(20)]
        public string? Phone { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;
        [Required]
        [StringLength(2)]
        public string StateCode { get; set; } = string.Empty;
        [Required]
        [StringLength(9)]
        public string ZipCode {  get; set; } = string.Empty;
        [StringLength(30)]
        public string? RepFirstName {  get; set; } = string.Empty;
        [StringLength(30)]
        public string? RepLastName { get; set;} = string.Empty;
        [Column(TypeName="Decimal(11,2)")]
        public decimal SalesTotal { get; set; } = 0;
    }
}
