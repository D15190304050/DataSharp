using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningConsole
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //FunctionalityTest.SetEqualityTest();
            //FunctionalityTest.BuildSets();

            string[] strings = { "A", "B", "C", "D", "E" };
            int numSelection = 3;
            var result = FunctionalityTest.Combinations(strings, numSelection);
            foreach (string[] s in result)
                FunctionalityTest.PrintIEnumerable(s);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}