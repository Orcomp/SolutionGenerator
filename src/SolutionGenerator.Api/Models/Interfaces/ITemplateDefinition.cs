// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateDefinitionBase.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Windows;

    public interface ITemplateDefinition
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }
        FrameworkElement GetView();
    }
}