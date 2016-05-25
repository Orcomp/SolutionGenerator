// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PluginCleanupService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.Extensibility
{
    using System;
    using System.IO;
    using System.Linq;
    using Catel.Logging;

    public class PluginCleanupService : IPluginCleanupService
    {
        private const string DeleteMeFilter = "*.deleteme";
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public bool IsCleanupRequired(string directory)
        {
            var deleteMeFile = GetDeleteMeFile(directory);
            return !string.IsNullOrWhiteSpace(deleteMeFile);
        }

        public void Cleanup(string directory)
        {
            Log.Debug("Cleaning up plugin at '{0}'", directory);

            if (!IsCleanupRequired(directory))
            {
                Log.Debug("Cleaning up of plugin is not required");
                return;
            }

            var deleteMeFile = GetDeleteMeFile(directory);
            var succeeded = false;

            try
            {
                Directory.Delete(directory, true);
                succeeded = !Directory.Exists(directory);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to clean up plugin at '{0}'", directory);
            }

            if (!succeeded)
            {
                try
                {
                    // Create the file again, we didn't delete successfully
                    if (!File.Exists(deleteMeFile))
                    {
                        using (File.Create(deleteMeFile))
                        {
                            // No need to write contents
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private string GetDeleteMeFile(string directory)
        {
            try
            {
                var files = Directory.GetFiles(directory, DeleteMeFilter, SearchOption.TopDirectoryOnly);
                return files.FirstOrDefault();
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}