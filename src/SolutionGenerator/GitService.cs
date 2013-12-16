namespace SolutionGenerator
{
    using System.IO;

    public class GitService
    {
        public static void InitGitRepository(DirectoryInfo directory)
        {
            LibGit2Sharp.Repository.Init(directory.FullName);
        }
    }
}
