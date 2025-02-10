using System.Text.RegularExpressions;

namespace Source.Utils.Validations
{
    public static class InputValidator
    {
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return emailRegex.IsMatch(email) && email.Length <= 254;
        }

        public static bool IsValidName(string name)
        {
            return name.Length is >= 3 and <= 30 && Regex.IsMatch(name, "^[a-zA-Z0-9_-]+$");
        }

        public static bool IsValidPassword(string password)
        {
            return password.Length >= 6;
        }
        
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length >= 10 && Regex.IsMatch(phoneNumber, "^\\d{10}$");
        }
    }
}
