namespace SolutionGenerator
{
    using System.IO;

    public class DirectoryCreator
    {
        public DirectoryCreator(FileInfo folderConfigFile, DirectoryInfo rootFolder)
        {
            FolderConfigFile = folderConfigFile;
            RootFolder = rootFolder;
        }

        public FileInfo FolderConfigFile { get; set; }
        public DirectoryInfo RootFolder { get; set; }
        
        public void CreateDirectoryStructure()
        {
            string[] directoriesToCreate = File.ReadAllLines(FolderConfigFile.FullName);

            foreach (string dir in directoriesToCreate)
            {
                RootFolder.CreateSubdirectory(dir);
            }
        }
    }
}
