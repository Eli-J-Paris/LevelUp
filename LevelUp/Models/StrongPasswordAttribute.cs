using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class StrongPasswordAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string password)
        {
            // Add your password criteria here.
            if (password.Length < 8)
            {
                ErrorMessage = "Password must be at least 8 characters long.";
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                ErrorMessage = "Password must contain at least one uppercase letter.";
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                ErrorMessage = "Password must contain at least one digit.";
                return false;
            }

            // Add more criteria as needed.
        }

        return true;
    }
}