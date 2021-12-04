using System.Globalization;
using System.Windows.Controls;

namespace Project_Groceries.ValidationRules
{
    class ValueRequiredRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) =>
            value != null && (value as string) != string.Empty
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "Value is required");
    }
}
