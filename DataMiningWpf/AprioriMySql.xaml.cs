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
using MySql.Data.MySqlClient;
using DataMiningConsole;

namespace DataMiningWpf
{
    /// <summary>
    /// AprioriMySql.xaml 的交互逻辑
    /// </summary>
    public partial class AprioriMySql : Window
    {
        /// <summary>
        /// A MySqlConnection instance used to connect to the SQL Server.
        /// </summary>
        private MySqlConnection conn;

        /// <summary>
        /// A MySqlDataAdapter to fetch data from SQL Server.
        /// </summary>
        private MySqlDataAdapter da;

        /// <summary>
        /// A clear command that can clear all the contents in the Transaction table.
        /// </summary>
        private MySqlCommand cmdClear;

        /// <summary>
        /// A DataSet instance that stores the data retrived from SQL Server.
        /// </summary>
        private DataSet ds;

        /// <summary>
        /// The number of rows in the Transaction table.
        /// </summary>
        private int numRows;

        /// <summary>
        /// The total number of items in the Transaction table.
        /// </summary>
        private int numItems;

        /// <summary>
        /// The minimum support count applied to the Apriori algorithm.
        /// </summary>
        private int minSupportCount;

        /// <summary>
        /// The minimum confidence applied to association rules.
        /// </summary>
        private double minConfidence;

        /// <summary>
        /// A LinkedList&lt;T> instance that stores all the frequent itemsets extracted by the Apriori algorithm.
        /// </summary>
        /// <remarks>
        /// The SortedSet&lt;string> class here represents the itemset that stores k items where k = 1, 2, 3, ...
        /// The Dictionary&lt;SortedSet&lt;string>, int> class here represents the collection of the KeyValuePair that associate the itemset with its occurance.
        /// The LinkedList&lt;Dictionary&lt;SortedSet&lt;string>, int>> class here represents the collection of the Dictionary&lt;TKey, TValue> mentioned above.
        /// </remarks>
        private LinkedList<Dictionary<SortedSet<string>, int>> frequentItemsets;

        /* String Literal for database processing. */

        /// <summary>
        /// The connection string used to connect to the local SQL Server.
        /// </summary>
        /// <remarks>
        /// You can configure you own connection string here. You can also let users can type the needed info by changing the window XAML.
        /// </remarks>
        private const string connString = @"Server = localhost; User Id = DinoStark; Password = non-feeling; Database = Startup;";

        /// <summary>
        /// The SQL query command used to retrive all the data in the Transaction table.
        /// </summary>
        /// <remarks>
        /// You can configure you onw query command here. You can also let users can type the query command by changing the window XAML.
        /// </remarks>
        private const string queryAll = @"SELECT * FROM Transactions;";

        /// <summary>
        /// The SQL query command used to retrive all the shopping lists in the Transaction table.
        /// </summary>
        /// <remarks>
        /// You can configure you onw query command here. You can also let users can type the query command by changing the window XAML.
        /// </remarks>
        private const string queryShoppingLists = @"SELECT ShoppingList FROM Transactions;";

        /// <summary>
        /// The SQL delete command used to clear all the data in the Transaction table.
        /// </summary>
        private const string sqlClear = @"DELETE FROM Transactions WHERE TransactionID >= 1;";

        /// <summary>
        /// Initializes a new window for the Apriori algorithm test, where the back-end database is SQL Server.
        /// </summary>
        /// <remarks>
        /// You can configure you onw query command here. You can also let users can type the query command by changing the window XAML.
        /// </remarks>
        public AprioriMySql()
        {
            // Initialize the window described in XMAL.
            InitializeComponent();

            // Configure the objects used for manipulating database.
            conn = new MySqlConnection(connString);
            da = new MySqlDataAdapter(queryAll, conn);
            cmdClear = new MySqlCommand(sqlClear, conn);
            ds = new DataSet();

            // Initialize other private variables.
            numRows = 0;
            numItems = 0;
            frequentItemsets = null;
        }

        /// <summary>
        /// Tests the connection to the back-end database.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains state information and event data associated with the Click event.</param>
        private void cmdTestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                MessageBox.Show("Successfully connect to SQL Server.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Generates data, and then inserts them into back-end database, and then shows them on the window.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains state information and event data associated with the Click event.</param>
        private void cmdGenerateData_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing if there is something wrong with the input.
            if (!CanGenerateData())
                return;

            // Clear the data generated priviously before inserting new data.
            ClearPreviousData();

            // Genereate new data using specified number of rows and items.
            DataGenerator.GenerateTransactionsMySql(conn, numRows, numItems);

            // Retrive data from database.
            FetchData();

            // Use LinkedList<T> instance to store all the transactions to show on the window.
            LinkedList<Transaction> transactions = new LinkedList<Transaction>();

            // Traverse through all the rows in the retrived data table.
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Get the TransactionID.
                string transactionID = row[0].ToString();

                // Get the ShoppingList correspond to the TransactionID.
                string shoppingList = row[1].ToString();

                // Initialize a new instance of the Transaction.
                Transaction transaction = new Transaction(transactionID, shoppingList);

                // Add it to the collection of transactions.
                transactions.AddLast(transaction);
            }

            // Put generated transactions on the screen.
            lstTransactions.ItemsSource = transactions;
        }

        /// <summary>
        /// Computes the frequent itemsets according to the transaction data stored in the database, and then shows them on the window.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains state information and event data associated with the Click event.</param>
        private void cmdComputeFrequentItemsets_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing if there is something wrong with the input.
            if (!CanComputeFrequentItemsets())
                return;

            // Initialize an Apriori solver based on SQL Server with specified MySqlConnection and SQL query command.
            DataMiningConsole.AprioriMySql apriori = new DataMiningConsole.AprioriMySql(conn, queryShoppingLists);

            // Set the minimum support count for the frequent itemset computation.
            apriori.MinSupportCount = minSupportCount;

            // Get the frequent itemsets.
            frequentItemsets = apriori.ComputeFrequentItemsets();

            // Initialize an empty LinkedList<T> instance to store the FrequentItemsetData instances that will be used for the window control (the ListView).
            LinkedList<FrequentItemsetData> frequntItemstesData = new LinkedList<FrequentItemsetData>();

            // Traverse through all the frequent itemsets.
            foreach (Dictionary<SortedSet<string>, int> nextFrequentItemsets in frequentItemsets)
            {
                foreach (KeyValuePair<SortedSet<string>, int> frequentItemset in nextFrequentItemsets)
                {
                    // Initialize the FrequentItemsetData instance for presentation.
                    FrequentItemsetData itemsetData = new FrequentItemsetData(frequentItemset);

                    // Add it to the collection.
                    frequntItemstesData.AddLast(itemsetData);
                }
            }

            // Set the ItemsSource property so that the window control will be refreshed.
            // And then you can see the computed frequent itemsets on the window.
            lstFrequentItemsets.ItemsSource = frequntItemstesData;
        }

        /// <summary>
        /// Generates the association rules according to the frequent itemsets computed before, and then shows them on the window.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains state information and event data associated with the Click event.</param>
        private void cmdGenerateAssociationRules_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing if there is something wrong with the input.
            if (!CanGenerateSars())
                return;

            // Get the association rules generated according to the frequent itemsets computed before.
            IEnumerable<AssociationRule> associationRules = AssociationRules.GenerateStrongAssociationRules(frequentItemsets, minConfidence);

            // Set the ItemsSource property so that the window control will be refreshed.
            // And then you can see the generated association rules on the window.
            lstStrongAR.ItemsSource = associationRules;
        }

        /// <summary>
        /// Clears the ListView controls on the window and clears the transactions stored in the database.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains state information and event data associated with the Click event.</param>
        private void cmdClearData_Click(object sender, RoutedEventArgs e)
        {
            // Clear the transactions stored in the database.
            ClearPreviousData();

            // Set the ItemsSource of ListView controls to null so that there will be nothing in the control.
            lstTransactions.ItemsSource = null;
            lstFrequentItemsets.ItemsSource = null;
            lstStrongAR.ItemsSource = null;
        }

        /// <summary>
        /// Returns true if the current configuration can let this program generate transactions correctly, false otherwise.
        /// </summary>
        /// <returns>True if the current configuration can let this program generate transactions correctly, false otherwise.</returns>
        private bool CanGenerateData()
        {
            // Report the error message and return false if the text that represents the numebr of transactions cann't be parsed to an integer.
            if (!int.TryParse(txtNumTransactions.Text, out numRows))
            {
                MessageBox.Show("You must input a positive integer for the number of transactions.");
                return false;
            }

            // Report the error message and return false if the text that represents the numebr of items can't be parsed to an integer.
            if (!int.TryParse(txtNumItems.Text, out numItems))
            {
                MessageBox.Show("You must input a positive integer for the number of items.");
                return false;
            }

            // Report the error message and return false if the parsed integers aren't both positive.
            if ((numRows <= 0) || (numItems <= 0))
            {
                MessageBox.Show("The input integers for the number of transactions and the number of items must be positive.");
                return false;
            }

            // Return true if the input information can pass the validation here.
            return true;
        }

        /// <summary>
        /// Returns true if the current configuration can let this program compute frequent itemsets correctly, false otherwise.
        /// </summary>
        /// <returns>True if the current configuration can let this program compute frequent itemsets correctly, false otherwise.</returns>
        private bool CanComputeFrequentItemsets()
        {
            // Reprot the error message and return false if the text that represents the minimum support count can't be parsed to an integer.
            if (!int.TryParse(txtMinSupportCount.Text, out minSupportCount))
            {
                MessageBox.Show("You must input a positive integer for the minimum support count.");
                return false;
            }

            // Report the error message and return false if the value of minSupportCount < 0.
            if ((minSupportCount < 0))
            {
                MessageBox.Show("You must input a positive integer for the minimum support count.");
                return false;
            }

            // Report the error message and return false if the value of minSupportCount > numRows.
            if (minSupportCount > numRows)
            {
                MessageBox.Show("The value of minimum support count must be less than or equal to the number of transactions.");
                return false;
            }

            // Return true if the input information can pass the validation here.
            return true;
        }

        /// <summary>
        /// Returns true if the current configuration can let this program generate association rules correctly, false otherwise.
        /// </summary>
        /// <returns>True if the current configuration can let this program generate association rules correctly, false otherwise.</returns>
        private bool CanGenerateSars()
        {
            // Report the error message and return false if the text that represents the minimum confidence can't be parsed to a double value.
            if (!double.TryParse(txtMinConfidence.Text, out minConfidence))
            {
                MessageBox.Show("You must input a double value for the minimum confidence.");
                return false;
            }

            // Report the error message and return false if the text that represents the minimum confidence isn't in the range of [0, 1].
            if ((minConfidence < 0) || (minConfidence > 1))
            {
                MessageBox.Show("The minimum confidence must be in the range of [0, 1]");
                return false;
            }

            // Return true if the input information can pass the validation here.
            return true;
        }

        /// <summary>
        /// Clear all the transactions stored in the Transaction table in the database.
        /// </summary>
        private void ClearPreviousData()
        {
            // Try to execute the clear command and report the error message if an exception captured.
            try
            {
                conn.Open();
                cmdClear.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Fetch transactions stored in the database.
        /// </summary>
        private void FetchData()
        {
            // Clear the original DataTable stored in the DataSet instance.
            ds.Tables.Clear();

            // Try to retrive all the transactions and report the error message if an exception captured.
            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}