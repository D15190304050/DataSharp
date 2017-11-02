using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DataMiningWpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static SqlConnection SqlConnection { get; set; }

        public static MySqlConnection MySqlConnection { get; set; }
    }
}
