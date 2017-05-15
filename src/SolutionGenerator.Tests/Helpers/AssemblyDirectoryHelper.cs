// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyDirectoryHelper.cs" company="WildGums">
//   Copyright (c) 2012 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Tests
{
    using System;
    using Catel.IO;

    internal static class AssemblyDirectoryHelper
    {
        public static string GetCurrentDirectory()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            return directory;
        }

        public static string Resolve(string fileName)
        {
            return Path.Combine(GetCurrentDirectory(), fileName);
        }
    }
}