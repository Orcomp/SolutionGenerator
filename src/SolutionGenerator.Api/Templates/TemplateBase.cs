// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Template.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Reflection;

    public abstract class TemplateBase : ITemplate
    {
        public virtual List<string> GetKeys()
        {
            var properties = GetType().GetPropertiesEx();
            return (from property in properties
                    select property.Name).ToList();
        }

        public virtual string GetValue(string key)
        {
            var templateObject = PropertyHelper.GetPropertyValue(this, key, true);
            if (templateObject != null)
            {
                var value = ObjectToStringHelper.ToString(templateObject);
                return value;
            }

            return string.Empty;
        }
    }
}