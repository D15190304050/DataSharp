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

namespace DataAccessWpf
{
    /// <summary>
    /// BindToDataSet.xaml 的交互逻辑
    /// </summary>
    public partial class BindToDataSet : Window
    {
        private DataTable products;

        public BindToDataSet()
        {
            InitializeComponent();
        }

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDbDataSet.GetProducts();
            lstProducts.ItemsSource = products.DefaultView;
        }

        private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            ((DataRowView)lstProducts.SelectedItem).Row.Delete();
        }
    }
}
