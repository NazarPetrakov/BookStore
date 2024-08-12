using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Common.Validators
{
    public class ValidDateOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateOnly dateOnlyValue)
            {
                if (dateOnlyValue > DateOnly.FromDateTime(DateTime.Now))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field is not a valid date.";
        }
    }
}
