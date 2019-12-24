using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountLines
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Countlines filename");
                return;
            }
            string path = args[0];
            long totalSize = new System.IO.FileInfo(path).Length;

            int lines = 0;
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines++;
                    if (lines % 50 == 0)
                        Console.WriteLine($"{((double)bs.Position / (double)totalSize).ToString("P2")} ({lines})");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Lines: " + lines);
        }
    }
}
