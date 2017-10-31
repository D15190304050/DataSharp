using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningWpf
{
    /// <summary>
    /// The Transaction class represents transaction in the database for window presentation.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets the Transaction ID.
        /// </summary>
        public string TransactionID { get; private set; }

        /// <summary>
        /// Gets the shopping list of this transaction.
        /// </summary>
        public string ShoppingList { get; private set; }

        /// <summary>
        /// Initializes an instance of the Transaction class with specified transaction ID and shopping list.
        /// </summary>
        /// <param name="transactionID">The specified transaction ID.</param>
        /// <param name="shoppingList">The specified shopping list.</param>
        public Transaction(string transactionID, string shoppingList)
        {
            TransactionID = transactionID;
            ShoppingList = shoppingList;
        }
    }
}
