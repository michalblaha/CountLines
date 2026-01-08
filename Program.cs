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
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            int bytesRead;
            long totalBytesRead = 0;
            long nextReportAt = totalSize / 20; // Report every 5%

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        if (buffer[i] == '\n') lines++;
                    }

                    totalBytesRead += bytesRead;

                    // Report progress every 5%
                    if (totalBytesRead >= nextReportAt)
                    {
                        Console.WriteLine($"{((double)totalBytesRead / (double)totalSize).ToString("P2")} ({lines} lines)");
                        nextReportAt += totalSize / 20;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Lines: " + lines);
        }
    }
}
