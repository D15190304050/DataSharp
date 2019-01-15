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
using Mathematics;

// Unsolved:
// * MATLAB-style state-based interface.
// * Object-oriented interface.
// * XLabel, YLabel, Title, Legend
// * Axis scaling.
// * Color bar.
// * FillBetween(x value, lower y-bound, upper y-bound, color)
// * Meshgrid
// * Contour, which supports "contour label", color map,
// * Histogram
// * Histogram2D with color bar.
// * HexBin
// * Legend, set positions (upper/lower left/center/right)
// * Color limit (color range)
// * Color map.
// * subplots, make it easy to access and modify each subplot
// * ShareX, ShareY
// * GridSpecification
// * Text annotation, support coordinate transform, including ax.transData and ax.transAxes of python matplotlib.
// * Arrows
// * Fine-grained adjust on ticks.
// * Grid on.
// * Interactive 3D plot.
//    - Grid / Triangulated 3D surface.
// * High level plot API like seaborn.
// * PairPlot like seaborn.
// * Violin plot.
// * public Figure Subplots(int numRows, int numColumns)
// * ax.flat

namespace Plotting
{
    /// <summary>
    /// Scatter.xaml 的交互逻辑
    /// </summary>
    public partial class Scatter : Window
    {
        public double PointRadius { get; set; }
        public double PlottingIndent { get; set; }
        
        private LinkedList<DataPoint> points;

        public Scatter()
        {
            InitializeComponent();
            points = new LinkedList<DataPoint>();
        }

        public void Plot(double[] x, double[] y)
        {
            LengthCheck(x, y);
            points.Clear();
            for (int i = 0; i < x.Length; i++)
                points.AddLast(new DataPoint(x[i], y[i]));
            Plot();
        }

        public void Plot(IEnumerable<DataPoint> pointsToPlot)
        {
            if (points == null)
                throw new ArgumentNullException("pointsToPlot", "The collection of points to plot can't be null.");
            points.Clear();
            foreach (DataPoint point in pointsToPlot)
                points.AddLast(point);
            Plot();
            
        }

        private void Plot()
        {
            if ((points == null) || (points.Count == 0))
                return;

            //this.PlottingIndent = 0;
            double horizontalMin = points.Min(point => point.X);
            double horizontalMax = points.Max(point => point.X);
            double verticalMin = points.Min(point => point.Y);
            double verticalMax = points.Max(point => point.Y);

            double horizontalScaleFactor = (plottingCanvas.ActualWidth - this.PointRadius - 2 * this.PlottingIndent) / (horizontalMax - horizontalMin);
            double verticalScaleFactor = (plottingCanvas.ActualHeight - this.PointRadius - 2 * this.PlottingIndent) / (verticalMax - verticalMin);

            foreach (DataPoint point in points)
            {
                double topLeftX = (point.X - horizontalMin) * horizontalScaleFactor + this.PlottingIndent - this.PointRadius / 2;
                double topLeftY = (point.Y - verticalMin) * verticalScaleFactor + this.PlottingIndent - this.PointRadius / 2;
                //double topLeftY = plottingCanvas.ActualHeight - (point.Y - verticalMin) * verticalScaleFactor - this.PlottingIndent - this.PointRadius / 2;
                Ellipse circle = new Ellipse();
                circle.Width = this.PointRadius * 2;
                circle.Height = this.PointRadius * 2;
                circle.SetValue(Canvas.LeftProperty, topLeftX);
                //circle.SetValue(Canvas.TopProperty, topLeftY);
                circle.SetValue(Canvas.BottomProperty, topLeftY);
                circle.Fill = Brushes.LightBlue;
                //circle.ToolTip = point.ToString();
                circle.ToolTip = $"({point.X},{point.Y})";
                plottingCanvas.Children.Add(circle);
            }
        }

        private static void LengthCheck(double[] x, double[] y)
        {
            if (x == null)
                throw new ArgumentNullException("x", "X-coordinates can't be null.");
            if (y == null)
                throw new ArgumentNullException("y", "Y-coordinates can't be null.");
            if (x.Length != y.Length)
                throw new ArgumentException("The length of 2 collections must be equal.");
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Replot();
        }

        private void Replot()
        {
            if (points.Count == 0)
                return;

            double horizontalMin = points.Min(point => point.X);
            double horizontalMax = points.Max(point => point.X);
            double verticalMin = points.Min(point => point.Y);
            double verticalMax = points.Max(point => point.Y);

            double horizontalScaleFactor = (plottingCanvas.ActualWidth - 2 * PlottingIndent) / (horizontalMax - horizontalMin);
            double verticalScaleFactor = (plottingCanvas.ActualHeight - 2 * PlottingIndent) / (verticalMax - verticalMin);

            foreach (object o in plottingCanvas.Children)
            {
                
                
            }
        }

        public void SaveFigure(string filePath)
        {
        }
    }
}
