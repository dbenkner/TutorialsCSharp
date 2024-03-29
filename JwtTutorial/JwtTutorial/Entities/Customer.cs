using JwtLibrary.Entities;

namespace JwtTutorial.Entities
{
    public class Customer : ApplicationUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
