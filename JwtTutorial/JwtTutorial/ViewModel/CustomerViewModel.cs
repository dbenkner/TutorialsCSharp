using JwtLibrary.Entities;

namespace JwtTutorial.ViewModel
{
    public class CustomerViewModel:ApplicationUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
