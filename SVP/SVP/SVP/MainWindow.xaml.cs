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

        RungeKutta runge;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSimulate_Click(object sender, RoutedEventArgs e)
        {
            if (radioStream.IsChecked.HasValue)
            {
                if ((bool)radioStream.IsChecked)
                {

                    if (textSeeds.Text.Trim() != "" && textSteps.Text.Trim() != "" && textDelta.Text.Trim() != "" && position != null && position2 != null)
                    {

                        int numSeeds = int.Parse(textSeeds.Text);
                        int numSteps = int.Parse(textSteps.Text);
                        float delta = float.Parse(textDelta.Text);

                        runge = new RungeKutta(numSeeds, numSteps, delta, position, position2, vectorField);

                        runge.generate();

                        streamlineImage.Source = vectorField.createImage(runge.lines);

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

                        int numSeeds = int.Parse(textSeeds.Text);
                        int numSteps = int.Parse(textSteps.Text);
                        float delta = float.Parse(textDelta.Text);
                        float time = float.Parse(textTime.Text);

                        runge = new RungeKutta(numSeeds, numSteps, delta, position, position2, vectorField, time);

                        runge.generatePathLines();

                        streamlineImage.Source = vectorField.createImage(runge.lines);

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
            MLApp.MLApp matlab = new MLApp.MLApp();

            string current = Directory.GetCurrentDirectory();

            current = "cd '" + current.Substring(0, current.IndexOf("SVP")) + "Matlab'";
            matlab.Execute(current);

            matlab.Execute("A = " + getLineMatrix() + ";");
            matlab.Execute("A = A';");

            matlab.Execute("pca2");
        }

        private string getLineMatrix()
        {
            string returnString = "[";

            string xValues = "";
            string yValues = "";

            foreach (Line line in runge.lines)
            {
                foreach (Vec2 point in line.Points)
                {
                    xValues += point.X + " ";
                    yValues += point.Y + " ";
                }

                returnString += xValues + yValues + ";";
            }

            returnString += "]";

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
            vectorField = new VectorField();
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
    }
}
