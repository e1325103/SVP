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

        RungeKutta runge;

        Canvas[] clusterCanvas;

        MLApp.MLApp matlab;

        byte[,] colors;

        int numSeeds;
        int numSteps;
        float delta;
        float time;

        public MainWindow()
        {
            InitializeComponent();

            clusterCanvas = new Canvas[5];
            clusterCanvas[0] = clusterCanvas1;
            clusterCanvas[1] = clusterCanvas2;
            clusterCanvas[2] = clusterCanvas3;
            clusterCanvas[3] = clusterCanvas4;
            clusterCanvas[4] = clusterCanvas5;

            colors = new byte[5, 3];

            colors[0, 0] = 255;
            colors[0, 1] = 0;
            colors[0, 2] = 0;

            colors[1, 0] = 0;
            colors[1, 1] = 255;
            colors[1, 2] = 0;

            colors[2, 0] = 0;
            colors[2, 1] = 0;
            colors[2, 2] = 255;

            colors[3, 0] = 255;
            colors[3, 1] = 255;
            colors[3, 2] = 0;

            colors[4, 0] = 0;
            colors[4, 1] = 255;
            colors[4, 2] = 255;


            matlab = new MLApp.MLApp();
            string current = Directory.GetCurrentDirectory();

            current = "cd '" + current.Substring(0, current.IndexOf("SVP")) + "Matlab'";
            matlab.Execute(current);

            vectorField = new VectorField(lineCanvas.ActualWidth);
            travel = new Travel();
        }

        private void buttonSimulate_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();

            if (radioStream.IsChecked.HasValue)
            {
                if ((bool)radioStream.IsChecked)
                {

                    if (textSeeds.Text.Trim() != "" && textSteps.Text.Trim() != "" && textDelta.Text.Trim() != "" && position != null && position2 != null)
                    {

                        numSeeds = int.Parse(textSeeds.Text);
                        numSteps = int.Parse(textSteps.Text);
                        delta = float.Parse(textDelta.Text);

                        runge = new RungeKutta(numSeeds, numSteps, delta, position, position2, vectorField);

                        runge.generate();

                        streamlineImage.Source = vectorField.createImage();

                        foreach (Polyline polLine in vectorField.drawLines(runge.lines, Colors.Black, 0.5))
                        {
                            lineCanvas.Children.Add(polLine);
                            Canvas.SetTop(polLine, 0);
                            Canvas.SetLeft(polLine, 0);
                        }

                        buttonCluster.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Please enter all parameters and click twice for the rectangle!");
                    }
                }
                else
                {
                    if (textSeeds.Text.Trim() != "" && textSteps.Text.Trim() != "" && textDelta.Text.Trim() != "" && textTime.Text.Trim() != "" && position != null && position2 != null)
                    {

                        numSeeds = int.Parse(textSeeds.Text);
                        numSteps = int.Parse(textSteps.Text);
                        delta = float.Parse(textDelta.Text);
                        time = float.Parse(textTime.Text);

                        runge = new RungeKutta(numSeeds, numSteps, delta, position, position2, vectorField, time);

                        runge.generatePathLines();

                        foreach (Polyline polLine in vectorField.drawLines(runge.lines, Colors.Black, 0.5))
                        {
                            lineCanvas.Children.Add(polLine);
                            Canvas.SetTop(polLine, 0);
                            Canvas.SetLeft(polLine, 0);
                        }

                        buttonCluster.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Please enter all parameters and click twice for the rectangle!");
                    }
                }
            }
        }

        private void buttonCluster_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();

            int numClusters = int.Parse(textClusters.Text);

            matlab.Execute("highNumberSamples = 0;");

            matlab.Execute("numClusters = " + numClusters + ";");

            bool first = true;

            foreach (Line line in runge.lines)
            {
                string t = getLineMatrix(line);

                if (first)
                {
                    matlab.Execute("streamlines = [" + getLineMatrix(line));
                    first = false;
                }
                else
                {
                    matlab.Execute("streamlines = [streamlines; " + getLineMatrix(line));
                }
            }

            matlab.Execute("streamlines = streamlines';");

            matlab.Execute("calculateVariabilityLines");

            double[,] centerLines = matlab.GetVariable("reconCenterLines", "base");

            double[,] percentClusterPrev = matlab.GetVariable("percentCluster", "base");

            //streamlineImage.Source = vectorField.createImage(centerLines, percentCluster);

            double[] percentCluster = new double[percentClusterPrev.GetLength(0)];

            for (int i = 0; i < percentClusterPrev.GetLength(0); i++)
            {
                percentCluster[i] = percentClusterPrev[i, 0];
            }

            barPanel.Children.Clear();
            double countClusterTotal = matlab.GetVariable("countClusterTotal", "base");

            Rectangle[] rects = new Rectangle[percentClusterPrev.GetLength(0)];

            for (int i = 1; i <= numClusters; i++)
            {
                //matlab.Execute("sampleStreamlines" + i + " = sampleStreamlines" + i + "'");
                double[,] clusterLines = matlab.GetVariable("sampleStreamlines" + i, "base");

                double percent = percentCluster[i - 1];

                Rectangle rect = new Rectangle();
                rect.Width = barPanel.ActualWidth;
                rect.Height = barPanel.ActualHeight * percent;
                rect.Fill = new SolidColorBrush(Color.FromArgb(125, colors[i - 1, 0], colors[i - 1, 1], colors[i - 1, 2]));

                rects[i - 1] = rect;

                foreach (Polyline polLine in vectorField.drawCluster(
                                                clusterLines,
                                                Color.FromRgb(
                                                    colors[i - 1, 0],
                                                    colors[i - 1, 1],
                                                    colors[i - 1, 2]),
                                                4))
                {
                    clusterCanvas[i - 1].Children.Add(polLine);
                    Canvas.SetTop(polLine, 0);
                    Canvas.SetLeft(polLine, 0);
                }
            }

            Array.Sort(percentCluster, rects);

            foreach (Rectangle rect in rects)
            {
                barPanel.Children.Add(rect);
            }


            foreach (Polyline centerLine in vectorField.drawCenterLines(centerLines, percentClusterPrev, colors))
            {
                centerCanvas.Children.Add(centerLine);
                Canvas.SetTop(centerLine, 0);
                Canvas.SetLeft(centerLine, 0);
            }
        }

        private string getLineMatrix(Line line)
        {
            /* double[,] lineArray = new double[numSteps * 2, numSeeds];

             int currentSeed = 0;

             foreach (Line line in lines)
             {
                 int currentStep = 0;

                 foreach (Vec2 point in line.Points)
                 {
                     lineArray[currentStep, currentSeed] = point.X;
                     lineArray[currentStep + numSteps, currentSeed] = point.Y;

                     currentStep++;
                 }

                 currentSeed++;
             }*/

            // return lineArray;

            int columns = numSteps;

            int i = 0;

            string returnString = "";

            string xValues = "";
            string yValues = "";

            foreach (Vec2 point in line.Points)
            {
                xValues += point.X + " ";
                yValues += point.Y + " ";
                i++;
            }

            while (i < columns)
            {
                xValues += line.Points[line.Points.Count - 1].X + " ";
                yValues += line.Points[line.Points.Count - 1].Y + " ";
                i++;
            }

            returnString += xValues;
            returnString += yValues;
            returnString += "]";

            returnString = returnString.Replace(',', '.');

            return returnString;
        }


        private void streamlineImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (first)
            {
                //position = new Vec2((float)e.GetPosition(streamlineImage).X, (float)e.GetPosition(streamlineImage).Y);
                position = new Vec2((float)e.GetPosition(streamlineImage).Y, (float)e.GetPosition(streamlineImage).X);

                position = position / ((float)streamlineImage.ActualWidth);
                position = position * 500;

            }
            else
            {
                //Vec2 pos2 = new Vec2((float)e.GetPosition(streamlineImage).X, (float)e.GetPosition(streamlineImage).Y);
                //position2 = new Vec2(pos2.X, pos2.Y);

                Vec2 pos2 = new Vec2((float)e.GetPosition(streamlineImage).Y, (float)e.GetPosition(streamlineImage).X);
                position2 = new Vec2(pos2.X, pos2.Y);

                position2 = position2 / ((float)streamlineImage.ActualWidth);
                position2 = position2 * 500;
            }

            first = !first;
        }

        private void buttonPreview_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();

            vectorField.import("D:\\WindData\\Entpackt");
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

            PointCollection pointCol = new PointCollection();


            for (int i = 0; i < coastlat.GetLength(0); i++)
            {
                double x = (coastlon[i, 0] + 20) * 14;
                double y = (coastlat[i, 0] - 80) * 14;

                if (double.IsNaN(x))
                {
                    Polyline polLine = new Polyline();
                    polLine.Stroke = new SolidColorBrush(Colors.Black);
                    polLine.StrokeThickness = 0.5;

                    polLine.Points = pointCol;

                    lineCanvas.Children.Add(polLine);

                    pointCol = new PointCollection();
                }
                else
                {
                    pointCol.Add(new Point(x, y * -1));
                }
            }

            Polyline lastPolLine = new Polyline();
            lastPolLine.Stroke = new SolidColorBrush(Colors.Black);
            lastPolLine.StrokeThickness = 0.5;

            lastPolLine.Points = pointCol;

            lineCanvas.Children.Add(lastPolLine);

            buttonTravelSimulate.IsEnabled = true;
            textTravelClusters.IsEnabled = true;
            labelTravelClusters.IsEnabled = true;
        }

        private void buttonTravelSimulate_Click(object sender, RoutedEventArgs e)
        {
            matlab.Execute("simulateTravels;");
            
            matlab.Execute("connections(:, 1:2) = (connections(:, 1:2) + 20) * 14;");
            matlab.Execute("connections(:, 3:4) = (connections(:, 3:4) - 80) * -14;");

            double[,] travelLines = matlab.GetVariable("connections", "base");

            //double x = (coastlon[i, 0] + 20) * 16;
            //double y = (coastlat[i, 0] - 80) * 16;

            foreach (Polyline line in travel.drawLines(travelLines, Colors.Blue, 1))
            {
                centerCanvas.Children.Add(line);
                Canvas.SetTop(line, 0);
                Canvas.SetLeft(line, 0);
            }

            buttonTravelCluster.IsEnabled = true;
        }

        private void buttonTravelCluster_Click(object sender, RoutedEventArgs e)
        {
            int numClusters = int.Parse(textClusters.Text);

            matlab.Execute("numClusters = " + numClusters + ";");

            matlab.Execute("highNumberSamples = 1;");
            
            matlab.Execute("streamlines = connections;");

            matlab.Execute("streamlines = streamlines';");

            matlab.Execute("calculateVariabilityLines");

            double[,] centerLines = matlab.GetVariable("reconCenterLines", "base");

            double[,] percentClusterPrev = matlab.GetVariable("percentCluster", "base");

            //streamlineImage.Source = vectorField.createImage(centerLines, percentCluster);

            double[] percentCluster = new double[percentClusterPrev.GetLength(0)];

            for (int i = 0; i < percentClusterPrev.GetLength(0); i++)
            {
                percentCluster[i] = percentClusterPrev[i, 0];
            }

            barPanel.Children.Clear();
            double countClusterTotal = matlab.GetVariable("countClusterTotal", "base");

            Rectangle[] rects = new Rectangle[percentClusterPrev.GetLength(0)];

            /*for (int i = 1; i <= numClusters; i++)
            {
                //matlab.Execute("sampleStreamlines" + i + " = sampleStreamlines" + i + "'");
                double[,] clusterLines = matlab.GetVariable("sampleStreamlines" + i, "base");

                double percent = percentCluster[i - 1];

                Rectangle rect = new Rectangle();
                rect.Width = barPanel.ActualWidth;
                rect.Height = barPanel.ActualHeight * percent;
                rect.Fill = new SolidColorBrush(Color.FromArgb(125, colors[i - 1, 0], colors[i - 1, 1], colors[i - 1, 2]));

                rects[i - 1] = rect;

                foreach (Polyline polLine in vectorField.drawCluster(
                                                clusterLines,
                                                Color.FromRgb(
                                                    colors[i - 1, 0],
                                                    colors[i - 1, 1],
                                                    colors[i - 1, 2]),
                                                4))
                {
                    clusterCanvas[i - 1].Children.Add(polLine);
                    Canvas.SetTop(polLine, 0);
                    Canvas.SetLeft(polLine, 0);
                }
            }*/

            
            foreach (Polyline centerLine in vectorField.drawCenterLines(centerLines, percentClusterPrev, colors))
            {
                centerCanvas.Children.Add(centerLine);
                Canvas.SetTop(centerLine, 0);
                Canvas.SetLeft(centerLine, 0);
            }
        }
    }
}
