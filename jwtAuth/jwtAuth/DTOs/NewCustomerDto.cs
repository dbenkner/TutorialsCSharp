namespace jwtAuth.DTOs
{
    public class NewCustomerDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string StateCode {  get; set; } = string.Empty;  
        public string ZipCode { get; set; } = string.Empty;
        public string? RepFirstName {  get; set; }
        public string? RepLastName { get; set; }
    }
}
