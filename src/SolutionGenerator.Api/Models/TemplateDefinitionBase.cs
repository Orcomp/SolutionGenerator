// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateDefinition.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Collections.Generic;
    using System.Windows;
    using Catel.Data;

    public abstract class TemplateDefinitionBase : ITemplateDefinition
    {
        protected TemplateDefinitionBase(ITemplateContext templateContext)
        {
            TemplateContext = templateContext;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public ITemplateContext TemplateContext { get; private set; }

        public virtual List<ITemplateFile> GetTemplateFiles()
        {
            var resources = TemplateFileHelper.FindResources(GetType().Assembly, "Files");
            return resources;
        }

        public virtual IValidationContext Validate()
        {
            return new ValidationContext();
        }

        public abstract FrameworkElement GetView();
    }

    public abstract class TemplateDefinitionBase<TTemplateContext> : TemplateDefinitionBase
        where TTemplateContext : ITemplateContext, new()
    {
        protected TemplateDefinitionBase()
            : this(new TTemplateContext())
        {
        }

        protected TemplateDefinitionBase(ITemplateContext templateContext)
            : base(templateContext)
        {
        }
    }
}