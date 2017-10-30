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
using System.Data.SqlClient;

namespace DataAccessWpf
{
    /// <summary>
    /// ReadDatabaseDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ReadDatabaseDemo : Window
    {
        public ReadDatabaseDemo()
        {
            InitializeComponent();
        }

        private class Transaction
        {
            public string TransactionID { get; set; }
            public string ShoppingList { get; set; }

            public Transaction(string id, string list)
            {
                TransactionID = id;
                ShoppingList = list;
            }
        }

        private void cmdReadData_Click(object sender, RoutedEventArgs e)
        {
            LinkedList<Transaction> transactions = new LinkedList<Transaction>();
            DataSet ds = new DataSet();

            const string connectionString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";
            SqlConnection conn = new SqlConnection(connectionString);

            const string query = @"SELECT * FROM Transactions;";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);

            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string id = row[0].ToString();
                string list = row[1].ToString();
                transactions.AddLast(new Transaction(id, list));
            }

            lstTransactions.ItemsSource = transactions;
        }
    }
}
