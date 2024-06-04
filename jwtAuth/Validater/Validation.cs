using System.Text.RegularExpressions;

namespace Validater
{
    public class Validation
    {
        public static bool ValidateEmail(string? email)
        {
            if (email == null) return true;
            Regex regex = new Regex("^([A-Za-z0-9+-_~.%]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4})$");
            return regex.IsMatch(email);
        }
    }
}
