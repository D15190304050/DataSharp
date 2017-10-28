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
    /// BindingGroupValidation.xaml 的交互逻辑
    /// </summary>
    public partial class BindingGroupValidation : Window
    {
        private ICollection<Product> products;

        public BindingGroupValidation()
        {
            InitializeComponent();
        }

        private void lstProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productBindingGroup.CommitEdit();
        }

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDB.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            productBindingGroup.CommitEdit();
        }

        private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Make sure update has taken place.
            FocusManager.SetFocusedElement(this, (Button)sender);
        }
    }
}
