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
using System.Xml;

namespace DataAccessWpf
{
    /// <summary>
    /// BindToXmlDataProvider.xaml 的交互逻辑
    /// </summary>
    public partial class BindToXmlDataProvider : Window
    {
        public BindToXmlDataProvider()
        {
            InitializeComponent();

            XmlDataProvider provider = new XmlDataProvider();
            XmlDocument doc = new XmlDocument();
            doc.Load("store.xml");
            provider.Document = doc;
            provider.XPath = "/Products";
            Binding binding = new Binding();
            binding.XPath = "Products";
            binding.Source = provider;
            binding.Mode = BindingMode.TwoWay;
            lstProducts.SetBinding(ListBox.ItemsSourceProperty, binding);
        }
    }
}
