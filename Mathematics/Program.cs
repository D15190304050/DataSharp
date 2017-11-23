using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //FunctionalityTest.SetEqualityTest();
            //FunctionalityTest.BuildSetsDemo();
            //FunctionalityTest.CombinationsFunctionalityTest();
            //FunctionalityTest.GetSubsetsDemo();
            //FunctionalityTest.SetRemovalTest();
            //FunctionalityTest.AllSubsetsDemo();
            //FunctionalityTest.ComplementDemo();
            UnitTest.VectorTest();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}