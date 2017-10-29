using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using System.Collections.ObjectModel;

namespace DataAccessWpf
{
    /// <summary>
    /// ReadExcelDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ReadExcelDemo : Window
    {
        private const string fullFilePath = "../../HealthCare.xlsx";

        private class HealthCareInfo
        {
            public string ID { get; set; }
            public string HealthCareNumber { get; set; }

            public HealthCareInfo(string id, string healthCareNumber)
            {
                ID = id;
                HealthCareNumber = healthCareNumber;
            }
        }

        public ReadExcelDemo()
        {
            InitializeComponent();

            LoadExcelFile(fullFilePath);
        }

        private void LoadExcelFile(string fullFilePath)
        {
            const int ID = 6;
            const int HealthCareNumber = 18;

            DataSet ds = new DataSet();

            string connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + fullFilePath + "; Extended Properties = 'Excel 12.0; HDR=NO; IMEX=1';";
            OleDbConnection conn = new OleDbConnection(connectionString);
            string query = "SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(query, conn);

            try
            {
                conn.Open();

                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                da.Fill(ds, "[Sheet1$]");
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            // Fill the collection of products.
            LinkedList<HealthCareInfo> healthInfo = new LinkedList<HealthCareInfo>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string id = row[ID].ToString();
                string healthCareNumber = row[HealthCareNumber].ToString();
                healthInfo.AddLast(new HealthCareInfo(id, healthCareNumber));
            }

            lstStudents.ItemsSource = healthInfo;
        }
    }
}
