using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace paint_test
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.DefaultDrawingAttributes.Width = 30;
            MyCanvas.DefaultDrawingAttributes.Height = 30;
        }

        private void paint_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name == "pen")
                MyCanvas.EditingMode = InkCanvasEditingMode.Ink;

            if (button.Name == "clear")
                MyCanvas.Strokes.Clear();

            if (button.Name == "erased")
                MyCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void porownaj_Click(object sender, RoutedEventArgs e)
        {
            Save save = new Save();
            int width = (int)this.MyCanvas.ActualWidth;
            int height = (int)this.MyCanvas.ActualHeight;
            save.zapiszRysunek("savedimage", width, height, MyCanvas);

            PunktyKluczowe pkt = new PunktyKluczowe();
            IDictionary<string, double> dict = new Dictionary<string, double>();

            foreach (string fileName in Directory.GetFiles(@"litery\"))
            {
                dict.Add(fileName, pkt.porownajZBaza(fileName));
            }

            var sortedDict = from entry in dict orderby entry.Value ascending select entry;
            dict = sortedDict.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);

            textBlock1.Text = dict.Keys.ElementAt(0).Substring(7, 1);
            textBlock2.Text = dict.Keys.ElementAt(1).Substring(7, 1);
            textBlock3.Text = dict.Keys.ElementAt(2).Substring(7, 1);

            textBlockPlik1.Text = dict.Keys.ElementAt(0).Substring(7);
            textBlockPlik2.Text = dict.Keys.ElementAt(1).Substring(7);
            textBlockPlik3.Text = dict.Keys.ElementAt(2).Substring(7);

            textBlockProcent1.Text = dict.Values.ElementAt(0).ToString() + " % ";
            textBlockProcent2.Text = dict.Values.ElementAt(1).ToString() + " % ";
            textBlockProcent3.Text = dict.Values.ElementAt(2).ToString() + " % ";

        }

        private void zapisz_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }


        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            String input = InputTextBox.Text;
            Save save = new Save();
            int width = (int)this.MyCanvas.ActualWidth;
            int height = (int)this.MyCanvas.ActualHeight;
            save.zapiszRysunek(input, width, height, MyCanvas);
            InputTextBox.Text = String.Empty;
        }


        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            InputTextBox.Text = String.Empty;
        }

    }
}
