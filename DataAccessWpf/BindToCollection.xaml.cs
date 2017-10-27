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

namespace DataAccessWpf
{
    /// <summary>
    /// BindToCollection.xaml 的交互逻辑
    /// </summary>
    public partial class BindToCollection : Window
    {
        private ICollection<Product> products;

        public BindToCollection()
        {
            InitializeComponent();
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDB.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            products.Remove((Product)lstProducts.SelectedItem);
        }

        private void cmdAddProduct_Click(object sender, RoutedEventArgs e)
        {
            products.Add(new Product("0000", "?", 0, "?"));
        }

        private void txt_TextChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
