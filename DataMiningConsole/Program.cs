using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;
using System.Data.SqlClient;

namespace DataMiningConsole
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //FunctionalityTest.StringHashTest();
            //FunctionalityTest.FrequencyCounter();
            //FunctionalityTest.DictionaryUpdateDemo();
            //UnitTest.AprioriSqlServerUnitTest();
            //Console.WriteLine();
            UnitTest.AssociationRulesUnitTest();
            //UnitTest.AssociationRulesUnitTest(DataSourceOption.GenerateNewData, 20, 5);

            //UnitTest.AprioriMySqlUnitTest();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}