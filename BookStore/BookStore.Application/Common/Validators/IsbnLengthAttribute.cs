using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Common.Validators
{
    public class IsbnLengthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isbn = value as string;
            if (string.IsNullOrEmpty(isbn))
            {
                return ValidationResult.Success;
            }

            isbn = isbn.Replace("-", "").Replace(" ", "");

            if (isbn.Length == 10 && IsIsbn10Valid(isbn))
            {
                return ValidationResult.Success;
            }

            if (isbn.Length == 13 && IsIsbn13Valid(isbn))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid ISBN.");
        }

        private static bool IsIsbn10Valid(string isbn)
        {
            if (isbn.Length != 10 || !isbn.Take(9).All(char.IsDigit) || !(char.IsDigit(isbn[9]) || isbn[9] == 'X'))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += (10 - i) * (isbn[i] - '0');
            }

            sum += isbn[9] == 'X' ? 10 : isbn[9] - '0';
            return sum % 11 == 0;
        }

        private static bool IsIsbn13Valid(string isbn)
        {
            if (isbn.Length != 13 || !isbn.All(char.IsDigit))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += (isbn[i] - '0') * (i % 2 == 0 ? 1 : 3);
            }

            int checksum = (10 - sum % 10) % 10;
            return checksum == isbn[12] - '0';
        }
    }
}
