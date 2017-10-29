using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace DataAccessConsole.OfficeProgramming
{
    public static class WalkThroughDemo
    {
        private class Account
        {
            public int ID { get; set; }
            public double Balance { get; set; }
        }

        public static void Demo()
        {
            // Create a List of accounts.
            List<Account> bankAccounts = new List<Account>
            {
                new Account{ ID = 345678, Balance = 541.27 },
                new Account{ ID = 1230221, Balance = -127.44 }
            };

            // Display the list in an Excel spread-sheet.
            DisplayInExcel(bankAccounts);

            // Create a Word document that contains an icon that links to the spread-sheet.
            CreateIconInWordDoc();
        }

        private static void DisplayInExcel(IEnumerable<Account> accounts)
        {
            var excelApp = new Excel.Application();

            // Make the object visible.
            excelApp.Visible = true;

            /* Note: The excelApp.Workbooks.Add() is commented because when initializing an instance of Excel.Application, there will be an active Excel App. */
            // Create a new, empty workbook and add it to the collectio returned by property Wookbooks.
            // The new workbook becomes the active wookbook.
            // Add has an optional parameter for specifying a particular template.
            // Because no argument is sent in this example, Add() creates a new workbook.
            //excelApp.Workbooks.Add();

            // This example uses a single WorkSheet.
            Excel._Worksheet worksheet = excelApp.ActiveSheet;

            // Establis column headings in cells A1 and B1.
            worksheet.Cells[1, "A"] = "ID Number";
            worksheet.Cells[1, "B"] = "Current Balance";

            int row = 1;
            foreach (Account a in accounts)
            {
                row++;
                worksheet.Cells[row, "A"] = a.ID;
                worksheet.Cells[row, "B"] = a.Balance;
            }

            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();

            // Call to AutoFormat in C# 2010 or later. This statement replaces the 2 calls to AutoFit().
            worksheet.Range["A1", "B3"].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);

            // Put the spread-sheet contents on the clipboard.
            // The copy method has 1 optional parameter for specifying a destination.
            // Because no argument is sent, the destination is the clipboard.
            worksheet.Range["A1:B3"].Copy();
        }

        private static void CreateIconInWordDoc()
        {
            var wordApp = new Word.Application();
            wordApp.Visible = true;

            // The add method has 4 reference parameters, all of which are optional. C# allows you to omit arguments for them if the default values are what you want.
            wordApp.Documents.Add();

            // PasteSpecial() has 7 reference parameters, all of which are optional. This example uses named arguments to specify values for 2 parameters.
            // Although there are reference parameters, you don't need to use the ref keyword, or to create variables to send in arguments. You can send the values directly.
            wordApp.Selection.PasteSpecial(Link: true, DisplayAsIcon: true);
        }
    }
}
