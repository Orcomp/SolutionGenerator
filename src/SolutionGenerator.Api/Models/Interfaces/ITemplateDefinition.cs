// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateDefinitionBase.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Collections.Generic;
    using System.Windows;
    using Catel.Data;

    public interface ITemplateDefinition
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }

        ITemplateContext TemplateContext { get; }

        FrameworkElement GetView();
        IValidationContext Validate();
        List<ITemplateFile> GetTemplateFiles();
    }
}