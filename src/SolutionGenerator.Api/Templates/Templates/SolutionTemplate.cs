// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using Catel.Data;

    public class SolutionTemplate : TemplateBase
    {
        public SolutionTemplate()
        {
            
        }

        public string Name { get; set; }

        public string Directory { get; set; }

        public override IValidationContext Validate()
        {
            var validationContext = new ValidationContext();

            if (string.IsNullOrWhiteSpace(Name))
            {
                validationContext.AddFieldValidationResult(FieldValidationResult.CreateError("Solution.Name", "The solution name is required"));
            }

            if (string.IsNullOrWhiteSpace(Directory))
            {
                validationContext.AddFieldValidationResult(FieldValidationResult.CreateError("Solution.Directory", "The solution directory is required"));
            }

            return validationContext;
        }
    }
}