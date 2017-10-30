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
using DataMiningConsole;

namespace DataMiningWpf
{
    /// <summary>
    /// AprioriSqlServer.xaml 的交互逻辑
    /// </summary>
    public partial class AprioriSqlServer : Window
    {
        /// <summary>
        /// A SqlConnection instance used to connect to the SQL Server.
        /// </summary>
        private SqlConnection conn;

        /// <summary>
        /// A SqlDataAdapter to fetch data from SQL Server.
        /// </summary>
        private SqlDataAdapter da;

        /// <summary>
        /// A clear command that can clear all the contents in the Transaction table.
        /// </summary>
        private SqlCommand cmdClear;

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
        private const string connString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";

        /// <summary>
        /// The SQL query command used to retrive all the data in the Transaction table.
        /// </summary>
        private const string queryAll = @"SELECT * FROM Transactions;";

        /// <summary>
        /// The SQL query command used to retrive all the shopping lists in the Transaction table.
        /// </summary>
        private const string queryShoppingLists = @"SELECT ShoppingList FROM Transactions;";

        /// <summary>
        /// The SQL delete command used to clear all the data in the Transaction table.
        /// </summary>
        private const string sqlClear = @"DELETE FROM Transactions WHERE TransactionID >= 1;";

        /// <summary>
        /// Initializes a new window for the Apriori algorithm test, where the back-end database is SQL Server.
        /// </summary>
        public AprioriSqlServer()
        {
            // Initialize the window described in XMAL.
            InitializeComponent();
            
            // Configure the objects used for manipulating database.
            conn = new SqlConnection(connString);
            da = new SqlDataAdapter(queryAll, conn);
            cmdClear = new SqlCommand(sqlClear, conn);
            ds = new DataSet();

            // Initialize other private variables.
            numRows = 0;
            numItems = 0;
            frequentItemsets = null;
        }

        /// <summary>
        /// Generate data, and then insert them into back-end database, and then show them on the window.
        /// </summary>
        /// <param name="sender">The Button instance associated with this method.</param>
        /// <param name="e">The RoutedEventArgs that contains info of the Click event.</param>
        private void cmdGenerateData_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing if there is something wrong with the input.
            if (!CanGenerateData())
                return;

            // Clear the data generated priviously before inserting new data.
            ClearPreviousData();

            // Genereate new data using specified number of rows and items.
            DataGenerator.GenerateTransactionsSqlServer(conn, numRows, numItems);

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
        /// 
        /// </summary>
        /// <remarks>
        /// The variables aren't used, and they don't have any concrete meaning, so the comments for them are omitted.
        /// </remarks>
        private void cmdComputeFrequentItemsets_Click(object sender, RoutedEventArgs e)
        {
            if (!CanComputeFrequentItemsets())
                return;

            DataMiningConsole.AprioriSqlServer apriori = new DataMiningConsole.AprioriSqlServer(conn, queryShoppingLists);
            apriori.MinSupportCount = minSupportCount;
            frequentItemsets = apriori.ComputeFrequentItemsets();
            LinkedList<FrequentItemsetData> frequntItemstesData = new LinkedList<FrequentItemsetData>();
            foreach (Dictionary<SortedSet<string>, int> nextFrequentItemsets in frequentItemsets)
            {
                foreach (KeyValuePair<SortedSet<string>, int> frequentItemset in nextFrequentItemsets)
                {
                    FrequentItemsetData itemsetData = new FrequentItemsetData(frequentItemset);
                    frequntItemstesData.AddLast(itemsetData);
                }
            }

            lstFrequentItemsets.ItemsSource = frequntItemstesData;
        }

        private void cmdGenerateAssociationRules_Click(object sender, RoutedEventArgs e)
        {
            if (!CanGenerateSars())
                return;

            var associationRules = AssociationRules.GenerateStrongAssociationRules(frequentItemsets, minConfidence);
            lstStrongAR.ItemsSource = associationRules;
        }

        private void cmdClearData_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousData();
            lstTransactions.ItemsSource = null;
            lstFrequentItemsets.ItemsSource = null;
            lstStrongAR.ItemsSource = null;
        }

        private bool CanGenerateData()
        {
            if (!int.TryParse(txtNumTransactions.Text, out numRows))
            {
                MessageBox.Show("You must input a positive integer for the number of transactions.");
                return false;
            }
            if (!int.TryParse(txtNumItems.Text, out numItems))
            {
                MessageBox.Show("You must input a positive integer for the number of items.");
                return false;
            }
            return true;
        }

        private bool CanComputeFrequentItemsets()
        {
            if (!int.TryParse(txtMinSupportCount.Text, out minSupportCount))
            {
                MessageBox.Show("You must input a positive integer for the minimum support count.");
                return false;
            }

            if ((minSupportCount < 0))
            {
                MessageBox.Show("You must input a positive integer for the minimum support count.");
                return false;
            }

            if (minSupportCount > numRows)
            {
                MessageBox.Show("The value of minimum support count must be less than or equal to the number of transactions.");
                return false;
            }

            return true;
        }

        private bool CanGenerateSars()
        {
            if (!double.TryParse(txtMinConfidence.Text, out minConfidence))
            {
                MessageBox.Show("You must input a double value for the minimum confidence.");
                return false;
            }

            if ((minConfidence < 0) || (minConfidence > 1))
            {
                MessageBox.Show("The minimum confidence must be in the range of [0, 1]");
                return false;
            }

            return true;
        }

        private void ClearPreviousData()
        {
            try
            {
                conn.Open();
                cmdClear.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        private void FetchData()
        {
            ds.Tables.Clear();
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
        }
    }
}