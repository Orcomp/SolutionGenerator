// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="WildGums">
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
    using System.Text;
    using System.Web;
    using Catel;
    using Catel.Logging;
    using Templates;

    public static class ResourceHelper
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static void ExtractEmbeddedResource(Assembly assembly, string resourceName, string destinationFileName)
        {
            using (var fileStream = new FileStream(destinationFileName, FileMode.Create, FileAccess.Write))
            {
                ExtractEmbeddedResource(assembly, resourceName, fileStream);
            }
        }

        public static string ExtractEmbeddedResource(Assembly assembly, string resourceName)
        {
            using (var memoryStream = new MemoryStream())
            {
                ExtractEmbeddedResource(assembly, resourceName, memoryStream);

                memoryStream.Position = 0L;

                using (var streamReader = new StreamReader(memoryStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static void ExtractEmbeddedResource(Assembly assembly, string resourceName, Stream targetStream)
        {
            Log.Debug("Extracting embedded resource '{0}' from assembly '{1}'", resourceName, assembly.FullName);

            using (var resource = assembly.GetManifestResourceStream(resourceName))
            {
                if (resource == null)
                {
                    Log.Warning("Failed to extract embedded resource '{0}', possible names:");

                    foreach (var name in assembly.GetManifestResourceNames())
                    {
                        Log.Warning("  * {0}", name);
                    }
                    return;
                }

                resource.CopyTo(targetStream);
            }
        }

        public static List<ITemplateFile> FindEmbeddedResources(Assembly assembly, string resourceDirectory)
        {
            var fileInfo = new FileInfo(assembly.Location);
            var assemblyFileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);

            var pathToCut = $"{assemblyFileName}.{resourceDirectory}".Replace("\\", ".");

            var embeddedResources = assembly.GetManifestResourceNames()
                .Where(x => x.StartsWith(pathToCut))
                .Select(x => new EmbeddedResourceTemplateFile(assembly, x, GetResourceSubPath(x, pathToCut))).ToList<ITemplateFile>();

            return embeddedResources;
        }

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