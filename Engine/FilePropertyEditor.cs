using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Engine
{
    public class FilePropertyEditor : IFileEditor
    {
        private IEnumerable<VersionedFile> sources;

        public FilePropertyEditor(IEnumerable<VersionedFile> sources)
        {
            this.sources = sources;
        }

        public void Edit(string path, IEnumerable<Release> releases)
        {
            var current = releases.Last();
            foreach (var setting in sources)
            {
                var allFiles = Directory.GetFiles(path, setting.FileName, SearchOption.AllDirectories);
                foreach (var file in allFiles)
                {
                    var codeblocks = new List<string>();
                    foreach (string line in File.ReadLines(file, Encoding.UTF8))
                    {
                        if (line.StartsWith(setting.Preffix))
                        {
                            codeblocks.Add(string.Format("{0}{1}{2}", setting.Preffix,  current.Version, setting.Suffix));
                        }
                        else
                        {
                            codeblocks.Add(line);
                        }
                    }
                    SaveChanges(file, codeblocks);
                }
            }
        }

        private static void SaveChanges(string file, List<string> codeblocks)
        {
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
