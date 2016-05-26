// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrcComponentTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates.OrcComponent
{
    using System.Windows;
    using Views;

    public class OrcComponentTemplateDefinition : TemplateDefinitionBase<OrcComponentTemplateContext>
    {
        public override FrameworkElement GetView()
        {
            return new SettingsView();
        }
    }
}