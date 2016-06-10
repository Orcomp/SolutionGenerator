// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateFileHelper.embeddedresources.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Catel.Logging;

    public static partial class TemplateFileHelper
    {
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
    }
}