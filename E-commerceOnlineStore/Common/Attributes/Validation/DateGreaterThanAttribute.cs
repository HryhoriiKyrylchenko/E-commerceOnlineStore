using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Common.Attributes.Validation
{
    /// <summary>
    /// A custom validation attribute to ensure that a given date is greater than the value of another date property.
    /// </summary>
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateGreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="comparisonProperty">The name of the property to compare against.</param>
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        /// <summary>
        /// Validates that the current date is greater than the comparison property's date.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the validation succeeded.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Get the value of the comparison property
            var comparisonPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (comparisonPropertyInfo == null)
            {
                return new ValidationResult($"Property {_comparisonProperty} not found.");
            }

            var comparisonValue = comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

            // Ensure the comparison value and the current value are both valid DateTime objects
            if (value is DateTime currentDate && comparisonValue is DateTime comparisonDate)
            {
                if (currentDate <= comparisonDate)
                {
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {_comparisonProperty}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
