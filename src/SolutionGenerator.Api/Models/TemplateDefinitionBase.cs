// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateDefinition.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Catel.Data;
    using Catel.Reflection;
    using Templates;

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
            var validationContext = new ValidationContext();

            var templateProperties = (from property in GetType().GetPropertiesEx()
                                      where property.PropertyType.ImplementsInterfaceEx<ITemplate>()
                                      select property).ToList();

            foreach (var templateProperty in templateProperties)
            {
                var template = PropertyHelper.GetPropertyValue(this, templateProperty.Name, false) as ITemplate;
                if (template != null)
                {
                    var contextValidationContext = template.Validate();
                    validationContext.SynchronizeWithContext(contextValidationContext, true);
                }
            }

            return validationContext;
        }

        public virtual void PreGenerate()
        {

        }

        public virtual void PostGenerate()
        {

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