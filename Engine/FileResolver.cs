using file2objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine
{
    public class FileResolver : ISourceResolver
    {
        public IEnumerable<Release> Resolve(string path)
        {
            string[] allFiles;
            List<Release> releases;
            try
            {
                allFiles = Directory.GetFiles(path, "ReleaseTrack.txt");
                if (!allFiles.Any()) return null;
                releases = PlainTextReader.From(allFiles.First()).DelimitBy(ColumnDelimiter.Pipe).GetAListOf<Release>();
            }
            catch (Exception)
            {
                return null;
            }
            return releases;
        }
    }
}
