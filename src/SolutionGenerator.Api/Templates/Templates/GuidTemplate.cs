// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Catel.Data;

    public class GuidTemplate : ObservableObject, ITemplate
    {
        private readonly Dictionary<string, Guid> _guids = new Dictionary<string, Guid>();

        public string GetValue(string key)
        {
            lock (_guids)
            {
                Guid guid;

                if (!_guids.TryGetValue(key, out guid))
                {
                    guid = Guid.NewGuid();
                    _guids[key] = guid;
                }

                return guid.ToString();
            }
        }

        public ICollection GetCollectionValue(string key)
        {
            throw new NotSupportedException();
        }

        public List<string> GetKeys()
        {
            lock (_guids)
            {
                return _guids.Keys.ToList();
            }
        }

        public IValidationContext Validate()
        {
            var validationContext = new ValidationContext();
            return validationContext;
        }

        public List<KeyValuePair<string, Guid>> GetAllGuids()
        {
            lock (_guids)
            {
                return _guids.ToList();
            }
        }
    }
}