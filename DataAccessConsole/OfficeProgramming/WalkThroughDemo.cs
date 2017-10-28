using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace DataAccessConsole.OfficeProgramming
{
    public class WalkThroughDemo
    {
        private class Account
        {
            public int ID { get; set; }
            public double Balance { get; set; }
        }

        public static void Demo()
        {

        }

        private static void DisplayInExcel(IEnumerable<Account> accounts)
        {
            var excelApp = new Excel.Application();

            // Make the object visible.
            excelApp.Visible = true;

            // Create a new, empty workbook and add it to the collectio returned by property Wookbooks.
            // The new workbook becomes the active wookbook.
            // Add has an optional parameter for specifying a particular template.
            // Because no argument is sent in this example, Add() creates a new workbook.
            excelApp.Workbooks.Add();

            // This example uses a single WorkSheet.
            Excel._Worksheet worksheet = excelApp.ActiveSheet;

        }
    }
}
