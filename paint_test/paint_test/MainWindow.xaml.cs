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
using Rectangle = System.Drawing.Rectangle;

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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name == "save")
            {
                FileStream fs = File.Open("save.bin", FileMode.CreateNew);
                MyCanvas.Strokes.Save(fs);
                fs.Close();
            }

            if (button.Name == "porownaj")
            {
                // Save document
                string filename = "savedimage.jpg";
                //get the dimensions of the ink control
                int margin = (int)this.MyCanvas.Margin.Left;
                int width = (int)this.MyCanvas.ActualWidth - margin;
                int height = (int)this.MyCanvas.ActualHeight - margin;
                //render ink to bitmap
                RenderTargetBitmap rtb =
                new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                rtb.Render(MyCanvas);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }

            }
                
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("save.bin", FileMode.Open, FileAccess.Read))
            {
                StrokeCollection sc = new StrokeCollection(fs);
                this.MyCanvas.Strokes = sc;
            }
        }

        private void porownaj_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SaveButton_Click(button, e);
            PunktyKluczowe.punktyKluczowe();
            
        }
    }
}
