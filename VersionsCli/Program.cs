using Engine;
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
            var versioner = new Versioner();
            versioner.ApplyTo(args[0]);
        }
    }
}
