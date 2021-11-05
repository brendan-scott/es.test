using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Emergent.Code.Test.Models.ViewModels;

namespace Emergent.Code.Test.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VersionAttribute : ValidationAttribute
    {
        private static readonly Regex Regex = new(INTEGERS_PERIODS_PATTERN);
        private const string INTEGERS_PERIODS_PATTERN = "^\\d+(\\.\\d+)*$";
        private const string DEFAULT_ERROR_MESSAGE = "Invalid version, please adjust and then try again.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Basic model binding sanity check failed
            if (validationContext.ObjectInstance is not SoftwareViewModel viewModel)
                return new ValidationResult("Internal Server error.");
            
            if (string.IsNullOrEmpty(viewModel.Version))
                return new ValidationResult(DEFAULT_ERROR_MESSAGE);

            if (Regex.IsMatch(viewModel.Version))
                return viewModel.Version.Split('.').Length <= 3 ? ValidationResult.Success : new ValidationResult("Only the format '[major].[minor].[patch]' is supported, please adjust and then try again.");

            if (viewModel.Version.StartsWith("."))
                return new ValidationResult("The first char cannot be '.', please adjust and then try again.");

            if (viewModel.Version.EndsWith("."))
                return new ValidationResult("The last char cannot be '.', please adjust and then try again.");

            if (viewModel.Version.Any(char.IsLetter))
                return new ValidationResult("Currently, we do not support versions with a letters, please adjust and then try again.");

            if (viewModel.Normalized.Major < 0 || viewModel.Normalized.Minor < 0 || viewModel.Normalized.Patch < 0)
                return new ValidationResult("Only non-negative integer values are supported, please adjust and then try again.");

            // Generic error message
            return new ValidationResult(DEFAULT_ERROR_MESSAGE);
        }
    }
}