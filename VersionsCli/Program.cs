using Engine;
using file2objects;
using System;

namespace VersionsCli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Provide a path to your solution");
                return;
            }

            var settings = PlainTextReader.From(args[0] + "\\VersionedFiles.txt").DelimitBy(ColumnDelimiter.Pipe).GetAListOf<VersionedFile>();
            var versioner = new Versioner(new FileResolver(), new FilePropertyEditor(settings) );
            versioner.ApplyTo(args[0]);
        }
    }
}
