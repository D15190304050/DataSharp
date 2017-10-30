using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningWpf
{
    public class Transaction
    {
        public string TransactionID { get; private set; }
        public string ShoppingList { get; private set; }

        public Transaction(string transactionID, string shoppingList)
        {
            TransactionID = transactionID;
            ShoppingList = shoppingList;
        }
    }
}
