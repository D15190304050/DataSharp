using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DataAccessConsole.OfficeProgramming;

namespace DataAccessConsole
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //WalkThroughDemo.Demo();



            RenameDemo.Handle();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}
