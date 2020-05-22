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

            if (button.Name == "select")
                MyCanvas.EditingMode = InkCanvasEditingMode.Select;

            if (button.Name == "erased")
                MyCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if(button.Name == "save")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "InkCanvas Format|*.jpg";
                sfd.Title = "Save InkCanvas File";
                sfd.ShowDialog();
                if (sfd.FileName == "") return;
                FileStream fs = File.Open(sfd.FileName, FileMode.CreateNew);
                MyCanvas.Strokes.Save(fs);
                fs.Close();
            }

            if (button.Name == "porownaj")
            {
                Bitmap bitmap = DrawControlToBitmap(MyCanvas);
                bitmap.Save("siemanko.jpg");
                System.Diagnostics.Process.Start("siemanko.jpg");
            }
                
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("MyPicture.bin", FileMode.Open, FileAccess.Read))
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

        private static Bitmap DrawControlToBitmap(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            graphics.CopyFromScreen(rect.Location, Point.Empty, control.Size);
            return bitmap;
        }
    }
}
