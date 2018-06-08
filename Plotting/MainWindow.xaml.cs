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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace Plotting
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)e.OriginalSource;

            string content = cmd.Content.ToString();
            switch (content)
            {
                case "Scatter":
                    ScatterTest();
                    break;
                case "PlotDemo":
                    PlotTest();
                    break;
                default: break;
            }
        }

        private void ScatterTest()
        {
            Scatter scatter = new Scatter();
            scatter.PointRadius = 10;
            scatter.PlottingIndent = 20;
            double[] x = { 1, 2, 3, 4, 5};
            double[] y = { 5, 4, 3, 2, 1 };
            scatter.Show();
            scatter.Plot(x, y);
        }

        private void PlotTest()
        {
            PlotDemo plot = new PlotDemo();
            double[] x = new double[700];
            double[] y = new double[700];

            for (int i = 0; i < 700; i++)
            {
                x[i] = i * 0.01;
                y[i] = Math.Sin(x[i]);
            }

            plot.Show();
            plot.Plot(x, y);
        }
    }
}
