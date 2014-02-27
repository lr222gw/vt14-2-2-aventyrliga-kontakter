using Labb2._2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labb2._2
{
    public static class myValidationExtension
    {
        public static bool Validate(this Contact instans, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instans);

            validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(instans, validationContext, validationResults, true); //returnerar true/false beroende på om validationen går igenom
        }
    }
}