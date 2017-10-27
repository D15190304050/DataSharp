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
    /// ValidationTest.xaml 的交互逻辑
    /// </summary>
    public partial class ValidationTest : Window
    {
        private ICollection<Product> products;

        public ValidationTest()
        {
            InitializeComponent();
        }

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDB.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void validationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                MessageBox.Show(e.Error.ErrorContent.ToString());
        }

        private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Make sure update has taken place.
            FocusManager.SetFocusedElement(this, (Button)sender);
        }

        private void cmdGetExceptions_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            GetErrors(sb, gridProductDetails);
            string message = sb.ToString();
            if (message != "")
                MessageBox.Show("");
        }

        private void GetErrors(StringBuilder sb, DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                // Ignore strings and dependency objects that aren't elements.
                TextBox element = child as TextBox;
                if (element == null)
                    continue;

                if (Validation.GetHasError(element))
                {
                    sb.Append(element.Text + " has errors:\r\n");
                    foreach (ValidationError error in Validation.GetErrors(element))
                        sb.Append("  " + error.ErrorContent.ToString());
                }

                // Check the children of this object.
                GetErrors(sb, element);
            }
        }
    }
}
