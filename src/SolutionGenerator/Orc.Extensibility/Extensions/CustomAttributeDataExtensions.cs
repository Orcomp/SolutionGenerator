// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAttributeDataExtensions.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Catel;

    public static class CustomAttributeDataExtensions
    {
        public static object GetReflectionOnlyAttributeValue<TAttribute>(this IEnumerable<CustomAttributeData> customAttributes)
            where TAttribute : Attribute
        {
            var attribute = (from customAttributeData in customAttributes
                             where customAttributeData.Constructor.DeclaringType.Name.EqualsIgnoreCase(typeof(TAttribute).Name)
                             select customAttributeData).FirstOrDefault();
            if (attribute != null)
            {
                return attribute.ConstructorArguments[0].Value;
            }

            return null;
        }
    }
}