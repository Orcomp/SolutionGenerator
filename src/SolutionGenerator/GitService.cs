using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionGenerator
{
    public class GitService
    {
        public static void InitGitRepository(DirectoryInfo directory)
        {
            LibGit2Sharp.Repository.Init(directory.FullName);
        }
    }
}
