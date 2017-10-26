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
    /// BindProductObject.xaml 的交互逻辑
    /// </summary>
    public partial class BindProductObject : Window
    {
        public BindProductObject()
        {
            InitializeComponent();
        }

        private void cmdGetProduct_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtID.Text, out int ID))
            {
                try
                {
                    gridProductDetails.DataContext = App.StoreDB.GetProduct(ID);
                }
                catch
                {
                    MessageBox.Show("Error catching database.");
                }
            }
            else
                MessageBox.Show("Invalid ID.");
        }
    }
}
