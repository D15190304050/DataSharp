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
using System.Data.SqlClient;
using System.ComponentModel;

namespace DataMiningWpf
{
    /// <summary>
    /// SqlServerLogin.xaml 的交互逻辑
    /// </summary>
    public partial class SqlServerLogin : Window
    {
        public SqlServerLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Build the connection to SQL Server.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains state information and event data associated with the Click event.</param>
        private void cmdCheckConnection_Click(object sender, RoutedEventArgs e)
        {
            // Initliaze a connection object.
            SqlConnection conn = new SqlConnection();

            // Configure the connction string.

            // Get the individual fields.
            string server = "Server = " + txtServerName.Text + ";";
            string dbName = "Database = " + txtDatabase.Text + ";";
            string user = "User = " + txtUserName.Text + ";";
            string password = "Password = " + txtPassword.Password + ";";

            // Union them.
            string connString = server + user + password + dbName;
            conn.ConnectionString = connString;

            // correct is a flag indicating whether the connection is built.
            bool correct = false;

            // Build the SQL command for initailzing the database.
            string cmdInit = @"DROP TABLE IF EXISTS Transactions;

                             CREATE TABLE Transactions
                             (
                                 TransactionID int Primary Key,
                                 ShoppingList text Not Null
                             );";
            SqlCommand cmdInitialization = new SqlCommand(cmdInit, conn);

            // Try to build the connection.
            try
            {
                conn.Open();

                // Initialize the database for this program.
                cmdInitialization.ExecuteNonQuery();

                // Set the flag to true.
                correct = true;
            }
            catch (SqlException ex)
            {
                // Report the error message and set the flag to false.
                MessageBox.Show(ex.Message);

                // The connection object of this program will be null.
                App.MySqlConnection = null;
            }
            finally
            {
                conn.Close();
            }

            if (correct)
            {
                // Show the message and set the connection object for this program.
                MessageBox.Show("Connection success.");
                App.SqlConnection = conn;

                // Close this window after connect to database successfully.
                this.Close();
            }
        }
    }
}
