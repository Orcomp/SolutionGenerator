// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.IO;
    using System.Text;
    using Catel.Logging;

    public static partial class TemplateFileHelper
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private static string GetResourceSubPath(string embeddedResourcePath, string pathToCut)
        {
            var sb = new StringBuilder(embeddedResourcePath);

            sb.Replace(pathToCut, string.Empty, 0, sb.Length);
            sb.Replace(".", string.Empty, 0, 1);

            var lastDotIndex = sb.ToString().LastIndexOf(".");
            if (lastDotIndex >= 0)
            {
                sb.Replace('.', Path.DirectorySeparatorChar, 0, lastDotIndex);
            }

            return sb.ToString();
        }
    }
}