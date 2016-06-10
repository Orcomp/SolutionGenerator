// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateFileHelper.resources.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Web;
    using Catel;
    using Catel.Logging;

    public static partial class TemplateFileHelper
    {
        public static List<ITemplateFile> FindResources(Assembly assembly, string resourceDirectory)
        {
            var resourceContainerName = assembly.GetName().Name + ".g.resources";

            var expectedPrefix = $"{resourceDirectory}";
            if (!expectedPrefix.EndsWith("/"))
            {
                expectedPrefix += "/";
            }

            using (var stream = assembly.GetManifestResourceStream(resourceContainerName))
            {
                using (var reader = new System.Resources.ResourceReader(stream))
                {
                    var resources = (from resource in reader.Cast<DictionaryEntry>()
                                     let resourceKey = resource.Key as string
                                     where resourceKey != null && resourceKey.StartsWithIgnoreCase(expectedPrefix)
                                     select new ResourceTemplateFile(assembly, resourceKey, HttpUtility.UrlDecode(resourceKey).Substring(expectedPrefix.Length))).ToList<ITemplateFile>();
                    return resources;
                }
            }
        }

        public static string ExtractResource(Assembly assembly, string resourceName)
        {
            using (var memoryStream = new MemoryStream())
            {
                ExtractResource(assembly, resourceName, memoryStream);

                memoryStream.Position = 0L;

                using (var streamReader = new StreamReader(memoryStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static void ExtractResource(Assembly assembly, string resourceName, Stream targetStream)
        {
            Log.Debug("Extracting resource '{0}' from assembly '{1}'", resourceName, assembly.FullName);

            var resourceContainerName = assembly.GetName().Name + ".g";

            var resourceManager = new ResourceManager(resourceContainerName, assembly);

            using (var sourceStream = resourceManager.GetStream(resourceName))
            {
                if (sourceStream == null)
                {
                    Log.Warning("Failed to extract resource '{0}', possible names:");

                    foreach (var name in assembly.GetManifestResourceNames())
                    {
                        Log.Warning("  * {0}", name);
                    }
                    return;
                }

                sourceStream.CopyTo(targetStream);
            }

            resourceManager.ReleaseAllResources();
        }
    }
}