// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginInfo.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Extensibility
{
    using System;
    using System.Reflection;
    using Catel;
    using Catel.Reflection;

    public class PluginInfo : IPluginInfo
    {
        public PluginInfo(Type type)
        {
            Argument.IsNotNull(() => type);

            var assembly = type.Assembly;

            // Note: since reflection only, we need to have a custom reflection mechanism
            var customAssemblyAttributes = assembly.GetCustomAttributesData();

            Name = customAssemblyAttributes.GetReflectionOnlyAttributeValue<AssemblyTitleAttribute>() as string ?? assembly.Title();
            Version = customAssemblyAttributes.GetReflectionOnlyAttributeValue<AssemblyInformationalVersionAttribute>() as string ?? assembly.Version();
            Company = customAssemblyAttributes.GetReflectionOnlyAttributeValue<AssemblyCompanyAttribute>() as string;

            Location = assembly.Location;
            ReflectionOnlyType = type;
            FullTypeName = type.FullName;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string Company { get; set; }

        public string Customer { get; set; }

        public string Location { get; private set; }

        public string FullTypeName { get; private set; }

        public Type ReflectionOnlyType { get; private set; }

        public override string ToString()
        {
            var value = $"{Name} {Version}";

            if (!string.IsNullOrWhiteSpace(Company))
            {
                value += $", created by {Company}";
            }

            return value;
        }
    }
}