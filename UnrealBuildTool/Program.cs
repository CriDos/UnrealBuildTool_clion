using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnrealBuildTool
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var realPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/UnrealBuildTool_clion.exe";
            Process.Start(realPath, string.Join(" ", args))?.WaitForExit();

            if (!args.Any(s => s.StartsWith("-project=")))
                return;

            var findParam = args.Single(s => s.StartsWith("-project="));
            var projectFilePath = findParam.Split('=').Last();
            var cMakeListsFilePath = new FileInfo(projectFilePath).DirectoryName + "/CMakeLists.txt";

            if (File.Exists(cMakeListsFilePath))
            {
                var fileContent = File.ReadAllText(cMakeListsFilePath);
                var newFileContent = fileContent.Replace('\\', '/');
                File.WriteAllText(cMakeListsFilePath, newFileContent);
            }
        }
    }
}