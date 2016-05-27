// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidTemplate.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GuidTemplate : TemplateBase
    {
        private readonly Dictionary<string, Guid> _guids = new Dictionary<string, Guid>();

        public override string GetValue(string key)
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

        public List<KeyValuePair<string, Guid>> GetAllGuids()
        {
            lock (_guids)
            {
                return _guids.ToList();
            }
        }
    }
}