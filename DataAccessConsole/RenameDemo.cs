using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace DataAccessConsole
{
    /// <summary>
    /// The RenameDemo class provides static methods for rename.
    /// </summary>
    public static class RenameDemo
    {
        /// <summary>
        /// The output directory that will contain all the selected phoths with new names.
        /// </summary>
        private const string OutputDirectory = @"E:\Data\Selected Photos\";

        /// <summary>
        /// The directory that contains all the photos.
        /// </summary>
        private const string OriginalPhotos = @"E:\Data\Original Photos\";

        /// <summary>
        /// The path of the Excel file that stores the ID and the health care number of students.
        /// </summary>
        private const string TablePath = @"E:\Data\HealthCare.xlsx";

        /// <summary>
        /// Handle the rename problem.
        /// </summary>
        public static void Handle()
        {
            // Get the Dictionary object that contains all the key value pairs of students of CS.
            Dictionary<string, string> data = GetData();

            // Get all the photo files in the specified directory.
            FileInfo[] files = GetFiles();

            // Handle the rename problem if the files is not null.
            if (files != null)
            {
                // Traverse through all the photo files.
                foreach (FileInfo file in files)
                {
                    // The length of the photo file suffix is 4 (".jpg").
                    int suffixLength = 4;

                    // Get the id of the student from the file name.
                    string fileName = file.Name;
                    string id = fileName.Substring(0, fileName.Length - suffixLength);

                    // Copy the photo to the output directory if the id is contained in the Excel file.
                    if (data.ContainsKey(id))
                    {
                        // Get the name of the target photo file with changed name.
                        string outputFileName = OutputDirectory + data[id] + ".jpg";

                        // Continue of the output file exists.
                        if (new FileInfo(outputFileName).Exists)
                            continue;

                        // Get the name of the original photo file.
                        string originalPhotoName = OriginalPhotos + fileName;

                        // Copy the file.
                        File.Copy(originalPhotoName, outputFileName);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a Dictionary&lt;stinng, string> where the keys are the IDs of students and the values are the health care number of students.
        /// </summary>
        /// <returns>A Dictionary&lt;stinng, &lt;string> where the keys are the IDs of students and the values are the health care number of students.</returns>
        private static Dictionary<string, string> GetData()
        {
            // The column index of the ID is 6.
            const int ID = 6;

            // The column index of the health care number + name is 18.
            const int HealthCareNumber = 18;

            // Initialize an empty data set to store the data table in the Excel file.
            DataSet ds = new DataSet();

            // Initialize an OleDbConnection that can retrive data from Excel file (both .xls and .xlsx).
            string connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + TablePath + "; Extended Properties = 'Excel 12.0; HDR=NO; IMEX=1';";
            OleDbConnection conn = new OleDbConnection(connectionString);

            // Edit a query command that will retrive all the data in the table.
            string query = "SELECT * FROM [Sheet1$]";

            // Try to retrive data from the Excel file.
            try
            {
                conn.Open();

                // Fill the data table.
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                da.Fill(ds, "[Sheet1$]");
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            // Fill the collection of health care info.
            Dictionary<string, string> healthInfo = new Dictionary<string, string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Get the ID and the health care number.
                string id = row[ID].ToString();
                string healthCareNumber = row[HealthCareNumber].ToString();

                // Add them to the Directionary.
                healthInfo.Add(id, healthCareNumber);
            }

            return healthInfo;
        }

        /// <summary>
        /// Gets files in the specified directory.
        /// </summary>
        /// <returns>Files in the specified directory.</returns>
        private static FileInfo[] GetFiles()
        {
            // Use the DirectoryInfo object to handle the directory in the local driver.
            DirectoryInfo directory = null;

            // Use an array of FileInfo to represent all the photo files in the directory.
            FileInfo[] files = null;

            // Try to get all the photo files.
            try
            {
                // Initialize the DirectoryInfo object with specified path.
                directory = new DirectoryInfo(OriginalPhotos);

                // Get all the "*.jpg" files in the specified directory.
                files = directory.GetFiles("*.jpg");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return files;
        }
    }
}
