using System.Text.RegularExpressions;

namespace WindowsFormsUserControl.Validators
{
    public static class EmailValidator
    {
        public static bool ValidateEmail(string email)
        {
            // Regular expression for validating email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
