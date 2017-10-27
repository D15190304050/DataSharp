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
    /// BindToLinqFilteredCollection.xaml 的交互逻辑
    /// </summary>
    public partial class BindToLinqFilteredCollection : Window
    {
        private ICollection<Product> products;

        public BindToLinqFilteredCollection()
        {
            InitializeComponent();
        }

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDB.GetProductsFilteredWithLinq(Decimal.Parse(txtMinCost.Text));
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

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
