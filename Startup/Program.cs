using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] data = { 1, 2, 3, 4, 5 };

            for (int i = 0; i < data.Length; i++)
                Console.Write(data[i] + " ");
            Console.WriteLine();

            foreach (int i in data)
                Console.Write(i + " ");
            Console.WriteLine();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
