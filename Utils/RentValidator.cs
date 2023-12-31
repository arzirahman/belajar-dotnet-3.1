using System;
using System.ComponentModel.DataAnnotations;

namespace Coba_Net.Utils
{

    public class FlowCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime endDate = (DateTime)value;

            var propertyInfo = validationContext.ObjectType.GetProperty("StartDate");
            if (propertyInfo != null)
            {
                DateTime startDate = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance, null);
                if (endDate < startDate)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}