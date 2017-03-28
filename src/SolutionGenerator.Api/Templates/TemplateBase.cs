// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Template.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Data;
    using Catel.Reflection;

    /// <summary>
    /// Convenient implementation of the ITemplate interface that supports Catel properties out of the box.
    /// </summary>
    public abstract class TemplateBase : ModelBase, ITemplate
    {
        public virtual List<string> GetKeys()
        {
            var properties = GetType().GetPropertiesEx();
            return (from property in properties
                    select property.Name).ToList();
        }

        public ICollection GetCollectionValue(string key)
        {
            var templateObject = PropertyHelper.GetPropertyValue(this, key, true) as ICollection;
            return templateObject;
        }

        string ITemplate.GetValue(string key)
        {
            var templateObject = PropertyHelper.GetPropertyValue(this, key, true);
            if (templateObject != null)
            {
                var value = ObjectToStringHelper.ToString(templateObject);
                return value;
            }

            return string.Empty;
        }

        public virtual IValidationContext Validate()
        {
            var validationContext = new ValidationContext();

            return validationContext;
        }
    }
}