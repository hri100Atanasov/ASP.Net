using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NetMVC_Tutorial.Models
{
    public class ValidateCustomerAge : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            var customerBirthdate = (DateTime)customer.BirthDate;

            if (customer.BirthDate == null)
            {
                return new ValidationResult("Birthdate is required!");
            }

            if (customerBirthdate.AddYears(18).Date <= DateTime.Now.Date)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Customer should be at least 18 years old!");
            }
        }
    }
}