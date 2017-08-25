using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Engine
{
    public class FilePropertyEditor : IFileEditor
    {
        public void Edit(string path, IEnumerable<Release> releases)
        {
            var current = releases.Last();
            var allFiles = Directory.GetFiles(path, "AssemblyInfo.cs", SearchOption.AllDirectories);
            foreach (var file in allFiles)
            {
                var codeblocks = new List<string>();
                foreach (string line in File.ReadLines(file, Encoding.UTF8))
                {
                    if (line.StartsWith("[assembly: AssemblyVersion("))
                    {
                        codeblocks.Add(@"[assembly: AssemblyVersion(\""" + current.Version + @"\"")]");
                    }
                    else if (line.StartsWith("[assembly: AssemblyFileVersion("))
                    {
                        codeblocks.Add(@"[assembly: AssemblyFileVersion(""" + current.Version + @""")]");
                    }
                    else
                    {
                        codeblocks.Add(line);
                    }
                }
                using (var sw = new StreamWriter(file))
                {
                    foreach (string line in codeblocks)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
