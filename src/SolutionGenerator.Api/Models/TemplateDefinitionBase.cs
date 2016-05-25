// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateDefinition.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Windows;

    public abstract class TemplateDefinitionBase : ITemplateDefinition
    {
        public TemplateDefinitionBase()
        {
            
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public abstract FrameworkElement GetView();
    }
}