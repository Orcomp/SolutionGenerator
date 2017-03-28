// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Catel.Data;

    public interface ITemplate : INotifyPropertyChanged
    {
        ICollection GetCollectionValue(string key);
        string GetValue(string key);
        List<string> GetKeys();
        IValidationContext Validate();
    }
}