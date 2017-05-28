using System;
using System.Collections.Generic;
using System.IO;
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

namespace SVP
{
    /// <summary>
    /// Interactionlogic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool first = true;

        Vec2 position;
        Vec2 position2;
        VectorField vectorField;
        Travel travel;

        Util toPolylineConverter;

        RungeKutta runge;

        Canvas[] clusterCanvas;

        MLApp.MLApp matlab;

        int numSeeds;
        int numSteps;
        int numBasis;
        int numSamples;
        int numClusters;
        float delta;
        float time;
        float conf;
        int sizeSplats;
        float boundaryCoeff;

        /// <summary>
        /// Constructor of the Mainwindow. The matlab connection and other variables get initialized. 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            clusterCanvas = new Canvas[5];
            clusterCanvas[0] = clusterCanvas1;
            clusterCanvas[1] = clusterCanvas2;
            clusterCanvas[2] = clusterCanvas3;
            clusterCanvas[3] = clusterCanvas4;
            clusterCanvas[4] = clusterCanvas5;

            matlab = new MLApp.MLApp();
            string current = Directory.GetCurrentDirectory();

            current = "cd '" + current.Substring(0, current.IndexOf("SVP")) + "Matlab'";
            matlab.Execute(current);

            travel = new Travel();

            toPolylineConverter = new Util();
        }

        private void SetClusterFieldsEnabled(bool enabled)
        {
            this.textBasis.IsEnabled = enabled;
            this.labelBasis.IsEnabled = enabled;
            this.textBoundary.IsEnabled = enabled;
            this.labelBoundary.IsEnabled = enabled;
            this.textClusters.IsEnabled = enabled;
            this.labelClusters.IsEnabled = enabled;
            this.textSamples.IsEnabled = enabled;
            this.labelSamples.IsEnabled = enabled;
            this.textSplats.IsEnabled = enabled;
            this.labelSplat.IsEnabled = enabled;
            this.textConf.IsEnabled = enabled;
            this.labelConf.IsEnabled = enabled;
            this.labelClusters.IsEnabled = enabled;
            this.buttonCluster.IsEnabled = enabled;
        }

        /// <summary>
        /// This method is called after the Simulate button for the hurricane dataset gets pressed. It reads the 
        /// values of the textfields and starts the Runge Kutta Integration of stream- or pathlines depending on 
        /// the selected radiobutton. The lines are then plotted on the background map of florida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSimulate_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();

            clearClusterCanvas();

            if (textSeeds.Text.Trim() != "" && textSteps.Text.Trim() != "" && textDelta.Text.Trim() != "" && position != null && position2 != null)
            {
                numSeeds = int.Parse(textSeeds.Text);
                numSteps = int.Parse(textSteps.Text);
                delta = float.Parse(textDelta.Text.Replace('.', ','));
                time = 48 / numSteps;

                if ((bool)radioPath.IsChecked)
                {
                    if (textTime.Text.Trim() != "")
                    {
                        time = float.Parse(textTime.Text.Replace('.', ','));
                    }
                }

                runge = new RungeKutta(numSeeds, numSteps, delta, position, position2, vectorField, time);

                if ((bool)radioStream.IsChecked)
                {
                    runge.generate();
                }
                else
                {
                    runge.generatePathLines();
                }

                vectorField.rungeLines = runge.lines;

                streamlineImage.Source = vectorField.createImage();

                List<Line> transformedLines = vectorField.transformLines(runge.lines);
                List<Polyline> polyLines = Util.getPolyLines(transformedLines, Colors.Black, 0.5);

                foreach (Polyline polLine in polyLines)
                {
                    lineCanvas.Children.Add(polLine);
                    Canvas.SetTop(polLine, 0);
                    Canvas.SetLeft(polLine, 0);
                }

                SetClusterFieldsEnabled(true);

                Util.numSteps = numSteps;

                vectorField.freshySimulated = true;
            }
            else
            {
                MessageBox.Show("Please enter all parameters and click twice for the rectangle!");
            }
        }

        /// <summary>
        /// This methods clears the content of the canvas by clearing the children. (Canvas contain streamlines etc.)
        /// </summary>
        private void clearCanvas()
        {
            borderCanvas.Children.Clear();
            lineCanvas.Children.Clear();

            clearClusterCanvas();
        }

        /// <summary>
        /// This methods clears the content of the canvas of the clusters by clearing the children.
        /// </summary>
        private void clearClusterCanvas()
        {
            clusterCanvas1.Children.Clear();
            clusterCanvas2.Children.Clear();
            clusterCanvas3.Children.Clear();
            clusterCanvas4.Children.Clear();
            clusterCanvas5.Children.Clear();

            centerCanvas.Children.Clear();

            barPanel.Children.Clear();
        }

        /// <summary>
        /// This method is called after the Cluster button for the hurricane dataset gets pressed. It reads the values for the clustering and calls the function cluster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCluster_Click(object sender, RoutedEventArgs e)
        {
            if (textConf.Text.Trim() != "" && textClusters.Text.Trim() != "" && textBasis.Text.Trim() != "" && textSamples.Text.Trim() != "" && textSplats.Text.Trim() != "" && textBoundary.Text.Trim() != "")
            {
                numBasis = int.Parse(textBasis.Text);
                numSamples = int.Parse(textSamples.Text);
                numClusters = int.Parse(textClusters.Text);
                conf = float.Parse(textConf.Text.Replace('.', ','));
                sizeSplats = int.Parse(textSplats.Text);
                boundaryCoeff = float.Parse(textBoundary.Text.Replace('.', ','));

                if (numClusters > 0 && numClusters <= 5)
                {
                    if (buttonSimulate.IsEnabled)
                    {
                        cluster(vectorField);
                    }
                    else if (buttonTravelSimulate.IsEnabled)
                    {
                        cluster(travel);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter at least 1 and max 5 clusters");
                }
            }
            else
            {
                MessageBox.Show("Please enter all parameters!");
            }
            //centerCanvas.Children.Clear();
        }

        /// <summary>
        /// This method calls the matlab code, hands over the data to matlab and stores the results. Afterwards the
        /// clusters and the centerlines/medians are plotted and the rectangles for the stackpanel showing, how many of 
        /// the clustered streamlines are contained in the respective cluster.
        /// </summary>
        /// <param name="clusterObject">The object for the clustering (vectorfield, travel)</param>
        private void cluster(IClusterObject clusterObject)
        {
            clearClusterCanvas();

            lineCanvas.Children.Clear();
            barPanel.Children.Clear();

            executeMatlab(clusterObject);

            double[,] centerLines = matlab.GetVariable("reconCenterLines", "base");
            double[,] percentCluster2D = matlab.GetVariable("percentCluster", "base");
            double[] percentCluster = Util.Make1Dimensional(percentCluster2D);

            drawClusters(percentCluster, clusterObject);

            drawCenterLines(centerLines, percentCluster, clusterObject);

            addRects(Util.getRects(numClusters, percentCluster, barPanel.ActualWidth, barPanel.ActualHeight));
        }

        /// <summary>
        /// This method plotts the lines of each cluster in the respective transparent color.
        /// </summary>
        /// <param name="percentCluster">The array contaning a percentage value for each cluster saying, how many of the clustered streamlines lie within the cluster</param>
        /// <param name="clusterObject">The object for the clustering (vectorfield, travel)</param>
        private void drawClusters(double[] percentCluster, IClusterObject clusterObject)
        {
            for (int i = 1; i <= numClusters; i++)
            {
                double[,] clusterBoundary = matlab.GetVariable("boundary" + i, "base");

                List<Line> boundaryLine = Util.getLines(clusterBoundary);
                boundaryLine = clusterObject.transformLines(boundaryLine);

                Color color = Color.FromArgb(125,
                                                Util.colors[i - 1, 0],
                                                Util.colors[i - 1, 1],
                                                Util.colors[i - 1, 2]);

                List<Polyline> polyLines = Util.getPolyLines(boundaryLine, color, 2);
                polyLines.First().Fill = new SolidColorBrush(color);

                clusterCanvas[i - 1].Children.Add(polyLines.First());
                Canvas.SetTop(polyLines.First(), 0);
                Canvas.SetLeft(polyLines.First(), 0);
            }
        }

        /// <summary>
        /// This function adds colored rectangles to a stackpanel showing how many of the clustered streamlines are contained in the respective cluster.
        /// </summary>
        /// <param name="rects">The array of rectangles that shall be added to the stackpanel</param>
        private void addRects(Rectangle[] rects)
        {
            foreach (Rectangle rect in rects)
            {
                barPanel.Children.Add(rect);
            }
        }

        /// <summary>
        /// This method executes matlab codes and sets parameters for the clustering and starts the matlab script calculateVariabilityLines;
        /// </summary>
        /// <param name="clusterObject">The object for the clustering (vectorfield, travel)</param>
        private void executeMatlab(IClusterObject clusterObject)
        {
            clusterObject.executeMatlab(matlab);

            matlab.Execute("numBasis = " + numBasis + ";");
            matlab.Execute("numSamples = " + numSamples + ";");
            matlab.Execute("numClusters = " + numClusters + ";");
            matlab.Execute("splatSize = " + sizeSplats + ";");

            matlab.Execute(("convInter = " + conf + ";").Replace(',', '.'));
            matlab.Execute(("boundCoeff = " + boundaryCoeff + ";").Replace(',', '.'));

            matlab.Execute("calculateVariabilityLinesPar");
        }

        /// <summary>
        /// This method draws the centerlines/medians of the clusters. The more of the cluster streamlines are contained in the cluster, the thicker the median gets plotted.
        /// </summary>
        /// <param name="centerLines">A double array of the center lines of the clusters handed by matlab</param>
        /// <param name="percentCluster">The array contaning a percentage value for each cluster saying, how many of the clustered streamlines lie within the cluster</param>
        /// <param name="clusterObject">The object for the clustering (vectorfield, travel)</param>
        private void drawCenterLines(double[,] centerLines, double[] percentCluster, IClusterObject clusterObject)
        {
            List<Line> cenLines = Util.getLines(centerLines);
            List<Line> transfCenLinse = clusterObject.transformLines(cenLines);
            List<Polyline> polyCenterLines = Util.getPolyLinesWithPercentage(transfCenLinse, percentCluster, true);

            foreach (Polyline polCenter in polyCenterLines)
            {
                centerCanvas.Children.Add(polCenter);
                Canvas.SetTop(polCenter, 0);
                Canvas.SetLeft(polCenter, 0);
            }
        }

        /// <summary>
        /// This method stores points via mouse clicks on the image, specifying a rectangle for the seedpoint generation for the streamlines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void streamlineImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (first)
            {
                position = new Vec2((float)e.GetPosition(streamlineImage).Y, (float)e.GetPosition(streamlineImage).X);

                position = position / ((float)streamlineImage.ActualWidth);
                position = position * 500;

            }
            else
            {
                Vec2 pos2 = new Vec2((float)e.GetPosition(streamlineImage).Y, (float)e.GetPosition(streamlineImage).X);
                position2 = new Vec2(pos2.X, pos2.Y);

                position2 = position2 / ((float)streamlineImage.ActualWidth);
                position2 = position2 * 500;
            }

            first = !first;
        }

        /// <summary>
        /// This method is called after the Load button for the hurricane dataset gets pressed. It initializes the
        /// vector field and creates the green and blue background image showing florida and the sea.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPreview_Click(object sender, RoutedEventArgs e)
        {
            clearCanvas();

            buttonCluster.IsEnabled = false;

            vectorField = new VectorField(lineCanvas.ActualWidth);
            vectorField.import();
            streamlineImage.Source = vectorField.createImage();

            buttonSimulate.IsEnabled = true;
            textSeeds.IsEnabled = true;
            textSteps.IsEnabled = true;
            textDelta.IsEnabled = true;
            textClusters.IsEnabled = true;
            labelClusters.IsEnabled = true;
            labelDelta.IsEnabled = true;
            labelSeeds.IsEnabled = true;
            labelSteps.IsEnabled = true;

            radioPath.IsEnabled = true;
            radioStream.IsEnabled = true;

            buttonTravelSimulate.IsEnabled = false;
            SetClusterFieldsEnabled(false);
        }

        /// <summary>
        /// This method disables the texfields and labels for pathlines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioStream_Checked(object sender, RoutedEventArgs e)
        {
            if (radioStream != null && textTime != null && labelTime != null)
            {
                if (radioStream.IsChecked.HasValue)
                {
                    if ((bool)radioStream.IsChecked)
                    {
                        textTime.IsEnabled = false;
                        labelTime.IsEnabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// This method enables the texfields and labels for pathlines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioPath_Checked(object sender, RoutedEventArgs e)
        {
            if (radioPath != null && textTime != null && labelTime != null)
            {
                if (radioPath.IsChecked.HasValue)
                {
                    if ((bool)radioPath.IsChecked)
                    {
                        textTime.IsEnabled = true;
                        labelTime.IsEnabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// This method is called after the Load button for the travel dataset gets pressed. It draws the borders of the contries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTravelPreview_Click(object sender, RoutedEventArgs e)
        {
            clearCanvas();

            streamlineImage.Source = null;

            textTime.IsEnabled = false;
            labelTime.IsEnabled = false;

            buttonCluster.IsEnabled = false;

            buttonSimulate.IsEnabled = false;
            textSeeds.IsEnabled = false;
            textSteps.IsEnabled = false;
            textDelta.IsEnabled = false;
            textClusters.IsEnabled = false;
            labelClusters.IsEnabled = false;
            labelDelta.IsEnabled = false;
            labelSeeds.IsEnabled = false;
            labelSteps.IsEnabled = false;

            radioPath.IsEnabled = false;
            radioStream.IsEnabled = false;

            barPanel.Children.Clear();

            lineCanvas.Children.Clear();

            matlab.Execute("load coastlines;");

            double[,] coastlat = matlab.GetVariable("coastlat", "base");
            double[,] coastlon = matlab.GetVariable("coastlon", "base");

            lineCanvas.Children.Clear();
            borderCanvas.Children.Clear();

            List<Polyline> borderLines = Util.getBorders(coastlon, coastlat, Colors.Black, 0.5);

            foreach (Polyline borderLine in borderLines)
            {
                borderCanvas.Children.Add(borderLine);
            }

            buttonTravelSimulate.IsEnabled = true;
            SetClusterFieldsEnabled(false);
        }

        /// <summary>
        /// This method is called after the Simulate button for the travel dataset gets pressed. It calls the matlab script for the travel line generation and plotts them on the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTravelSimulate_Click(object sender, RoutedEventArgs e)
        {
            clearClusterCanvas();

            matlab.Execute("simulateTravels;");

            matlab.Execute("connections(:, 1:2) = (connections(:, 1:2) + 20) * 14;");
            matlab.Execute("connections(:, 3:4) = (connections(:, 3:4) - 80) * -14;");

            double[,] travelLines = matlab.GetVariable("connections", "base");

            List<Line> lines = Util.getLines(travelLines);

            travel.lines = lines;

            List<Polyline> polLines = Util.getPolyLines(lines, Colors.Blue, 1);

            foreach (Polyline line in polLines)
            {
                centerCanvas.Children.Add(line);
                Canvas.SetTop(line, 0);
                Canvas.SetLeft(line, 0);
            }

            SetClusterFieldsEnabled(true);
        }

        /// <summary>
        /// This method is called after the Cluster button for the coastlines/travel dataset gets pressed. It reads the values for the clustering and calls the function cluster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTravelCluster_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
