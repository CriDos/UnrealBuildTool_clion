using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnrealBuildTool
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var realPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                           "/UnrealBuildTool_clion.exe";
            Process.Start(realPath, string.Join(" ", args))?.WaitForExit();

            var findParams = args.Where(s => s.StartsWith("-project=")).ToArray();
            if (findParams.Length == 0)
                return;

            var projectFilePath = findParams.First().Split('=').Last();
            var cMakeListsFilePath = new FileInfo(projectFilePath).DirectoryName + "/CMakeLists.txt";

            if (!File.Exists(cMakeListsFilePath))
                return;

            var fileContent = File.ReadAllText(cMakeListsFilePath).Replace('\\', '/');
            File.WriteAllText(cMakeListsFilePath, fileContent);
        }
    }
}