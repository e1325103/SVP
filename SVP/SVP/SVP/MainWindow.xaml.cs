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
    /// Interaktionslogik für MainWindow.xaml
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
        int numClusters;
        float delta;
        float time;
        float conf;

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

                buttonCluster.IsEnabled = true;

                Util.numSteps = numSteps;

                vectorField.freshySimulated = true;
            }
            else
            {
                MessageBox.Show("Please enter all parameters and click twice for the rectangle!");
            }
        }

        private void clearCanvas()
        {
            borderCanvas.Children.Clear();
            lineCanvas.Children.Clear();                        

            clearClusterCanvas();
        }

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
    

        private void buttonCluster_Click(object sender, RoutedEventArgs e)
        {
            if (textConf.Text.Trim() != "" && textClusters.Text.Trim() != "")            {

                numClusters = int.Parse(textClusters.Text);
                conf = float.Parse(textConf.Text.Replace('.', ','));

                if (numClusters > 0 && numClusters <= 5)
                {
                    cluster(vectorField);
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
        }

        private void cluster(IClusterObject clusterObject)
        {
            clearClusterCanvas();

            lineCanvas.Children.Clear();
            barPanel.Children.Clear();            

            executeMatlab(clusterObject);

            double[,] centerLines = matlab.GetVariable("reconCenterLines", "base");
            double[,] percentCluster2D = matlab.GetVariable("percentCluster", "base");
            double countClusterTotal = matlab.GetVariable("countClusterTotal", "base");
            double[] percentCluster = Util.Make1Dimensional(percentCluster2D);

            drawClusters(percentCluster, clusterObject);

            drawCenterLines(centerLines, percentCluster, clusterObject);

            addRects(Util.getRects(numClusters, percentCluster, barPanel.ActualWidth, barPanel.ActualHeight));
        }

        private void drawClusters(double[] percentCluster, IClusterObject clusterObject)
        {
            for (int i = 1; i <= numClusters; i++)
            {
                double[,] clusterLines = matlab.GetVariable("sampleStreamlines" + i, "base");

                double percent = percentCluster[i - 1];

                List<Line> lines = Util.getLines(clusterLines);
                List<Line> transformedLines = clusterObject.transformLines(lines);              

                Color color = Color.FromArgb(   125, 
                                                Util.colors[i - 1, 0],
                                                Util.colors[i - 1, 1],
                                                Util.colors[i - 1, 2]);

                List<Polyline> polyLines = Util.getPolyLines(transformedLines, color, 4);

                foreach (Polyline polLine in polyLines)
                {
                    clusterCanvas[i - 1].Children.Add(polLine);
                    Canvas.SetTop(polLine, 0);
                    Canvas.SetLeft(polLine, 0);
                }
            }
        }        

        private void addRects(Rectangle[] rects)
        {
            foreach (Rectangle rect in rects)
            {
                barPanel.Children.Add(rect);
            }  
        }

        private void executeMatlab(IClusterObject clusterObject)
        {
            clusterObject.executeMatlab(matlab);

            matlab.Execute("numClusters = " + numClusters + ";");

            string ex = "convInter = " + conf + ";";

            matlab.Execute(("convInter = " + conf + ";").Replace(',', '.'));

            matlab.Execute("calculateVariabilityLines");
        }

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
            textTravelClusters.IsEnabled = false;
            labelTravelClusters.IsEnabled = false;

            buttonTravelCluster.IsEnabled = false;

            textConf.IsEnabled = true;
            labelConf.IsEnabled = true;
            textTravelConf.IsEnabled = false;
            labelTravelConf.IsEnabled = false;
        }

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
            textTravelClusters.IsEnabled = true;
            labelTravelClusters.IsEnabled = true;

            textConf.IsEnabled = false;
            labelConf.IsEnabled = false;
            textTravelConf.IsEnabled = true;
            labelTravelConf.IsEnabled = true;
        }

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

            buttonTravelCluster.IsEnabled = true;
        }

        private void buttonTravelCluster_Click(object sender, RoutedEventArgs e)
        {
            centerCanvas.Children.Clear();

            if (textTravelConf.Text.Trim() != "" && textTravelClusters.Text.Trim() != "")
            {

                numClusters = int.Parse(textTravelClusters.Text);
                conf = float.Parse(textTravelConf.Text.Replace('.', ','));

                if (numClusters > 0 && numClusters <= 5)
                {
                    cluster(travel);
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
        }
    }
}
