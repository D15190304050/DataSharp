using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DataAccessWpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static StoreDB storeDb = new StoreDB();

        public static StoreDB StoreDB { get { return storeDb; } }

        private static StoreDbDataSet storeDbDataSet = new StoreDbDataSet();

        public static StoreDbDataSet StoreDbDataSet { get { return storeDbDataSet; } }
    }
}
