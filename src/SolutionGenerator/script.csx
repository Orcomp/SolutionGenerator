#r "System.Core"
#r "System"
#r "System.Drawing"
#r "System.Windows.Forms"
#r "SolutionGenerator.dll"
#r "SolutionGenerator.Wpf.exe"

using System;
using System.Threading;
using SolutionGenerator;
using SolutionGenerator.Wpf;

public static class Utilities
{

        public static void RunInSTAThread(ThreadStart threadStart)
        {
                var thread = new Thread(threadStart);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
        }
}

Utilities.RunInSTAThread(() => new GeneratorForm().ShowDialog());