// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionItemTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Data;
    using Catel.Reflection;

    public class CollectionItemTemplate : ModelBase, ITemplate
    {
        private readonly object _collectionItem;
        private readonly Type _collectionItemType;

        public CollectionItemTemplate(object collectionItem)
        {
            Argument.IsNotNull("collectionItem", collectionItem);

            _collectionItem = collectionItem;
            _collectionItemType = collectionItem.GetType();
        }

        public ICollection GetCollectionValue(string key)
        {
            throw new NotImplementedException();
        }

        string ITemplate.GetValue(string key)
        {
            var value = PropertyHelper.GetPropertyValue(_collectionItem, key, true);
            return value != null ? value.ToString() : string.Empty;
        }

        public List<string> GetKeys()
        {
            return (from property in _collectionItemType.GetPropertiesEx()
                    select property.Name).ToList();
        }

        public IValidationContext Validate()
        {
            return new ValidationContext();
        }
    }
}