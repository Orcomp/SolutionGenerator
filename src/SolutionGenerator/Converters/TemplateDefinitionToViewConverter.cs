// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateDefinitionToViewConverter.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Converters
{
    using System;
    using Catel.MVVM.Converters;

    public class TemplateDefinitionToViewConverter : ValueConverterBase<ITemplateDefinition>
    {
        protected override object Convert(ITemplateDefinition value, Type targetType, object parameter)
        {
            if (value is null)
            {
                return null;
            }

            var view = value.GetView();
            return view;
        }
    }
}