﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MySql.Data.MySqlClient;

namespace DataMiningWpf
{
    /// <summary>
    /// MySqlLogin.xaml 的交互逻辑
    /// </summary>
    public partial class MySqlLogin : Window
    {
        public MySqlLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Build the connection to MySQL Server.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains state information and event data associated with the Click event.</param>
        private void cmdBuildConnection_Click(object sender, RoutedEventArgs e)
        {
            // Initliaze a connection object.
            MySqlConnection conn = new MySqlConnection();

            // Set the MySqlConnection of this program to null to force the user to login.
            App.MySqlConnection = null;

            // Configure the connction string.

            // Get the individual fields.
            string server = "Server = " + txtServerName.Text + ";";
            string dbName = "Database = " + txtDatabase.Text + ";";
            string user = "User Id = " + txtUserName.Text + ";";
            string password = "Password = " + txtPassword.Password + ";";

            // Union them.
            string connString = server + user + password + dbName;
            conn.ConnectionString = connString;

            // Build the SQL command for initailzing the database.
            string cmdInit = @"DROP TABLE IF EXISTS Transactions;

                             CREATE TABLE Transactions
                             (
                                 TransactionID int Primary Key,
                                 ShoppingList text Not Null
                             );";
            MySqlCommand cmdInitialization = new MySqlCommand(cmdInit, conn);

            // correct is a flag indicating whether the connection is built.
            bool correct = false;

            // Try to build the connection.
            try
            {
                conn.Open();

                // Initialize the database for this program.
                cmdInitialization.ExecuteNonQuery();

                // Set the flag to true.
                correct = true;
            }
            catch (MySqlException ex)
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
                App.MySqlConnection = conn;

                // Close this window after connect to database successfully.
                this.Close();
            }
        }
    }
}