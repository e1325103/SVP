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

namespace SVP
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSimulate_Click(object sender, RoutedEventArgs e)
        {
            VectorField vectorField = new VectorField();
            vectorField.import("D:\\WindData");
            streamlineImage.Source = vectorField.createImage(-20.0f, 20.0f);
        }

        private void buttonCluster_Click(object sender, RoutedEventArgs e)
        {
            MLApp.MLApp matlab = new MLApp.MLApp();
            matlab.Execute(@"cd D:\WindData\Visualisierung_2\Matlab");
            matlab.Execute("pca2");
        }
    }
}
