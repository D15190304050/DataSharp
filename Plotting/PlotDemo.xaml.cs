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

namespace Plotting
{
    /// <summary>
    /// PlotDemo.xaml 的交互逻辑
    /// </summary>
    public partial class PlotDemo : Window
    {
        private LinkedList<DataPoint> points;

        public PlotDemo()
        {
            points = new LinkedList<DataPoint>();
            InitializeComponent();
        }

        public void Plot(double[] x, double[] y)
        {
            LengthCheck(x, y);
            points.Clear();
            for (int i = 0; i < x.Length; i++)
                points.AddLast(new DataPoint(x[i], y[i]));
            Plot();
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

        private void Plot()
        {
            if ((points == null) || (points.Count == 0))
                return;

            //this.PlottingIndent = 0;
            double horizontalAxisMin = points.Min(point => point.X);
            double horizontalAxisMax = points.Max(point => point.X);
            double verticalAxisMin = points.Min(point => point.Y);
            double verticalAxisMax = points.Max(point => point.Y);

            double horizontalScaleFactor = plottingCanvas.ActualWidth / (horizontalAxisMax - horizontalAxisMin);
            double verticalScaleFactor = plottingCanvas.ActualHeight / (verticalAxisMax - verticalAxisMin);

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();

            pathFigure.StartPoint = new Point(points.First.Value.X, points.First.Value.Y);
            PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();


            foreach (DataPoint point in points)
            {
                double topLeftX = (point.X - horizontalAxisMin) * horizontalScaleFactor;
                double topLeftY = plottingCanvas.ActualHeight - (point.Y - verticalAxisMin) * verticalScaleFactor;

                LineSegment lineSegment = new LineSegment();
                lineSegment.Point = new Point(topLeftX, topLeftY);
                pathSegmentCollection.Add(lineSegment);
            }

            pathFigure.Segments = pathSegmentCollection;
            pathGeometry.Figures.Add(pathFigure);
            plottingPath.Data = pathGeometry;
        }
    }
}
