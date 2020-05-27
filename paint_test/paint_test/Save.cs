using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace paint_test
{
    class Save
    {

        public void zapiszRysunek(String nazwaPliku,int width,int height, System.Windows.Media.Visual MyCanvas)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "savedimage.jpg";
            int krotnoscPliku = 0;
            string filename;

            if (nazwaPliku != "savedimage")
            {
                if (File.Exists(@"litery\" + nazwaPliku + ".jpg"))
                    do
                    {
                        krotnoscPliku++;
                    } while (File.Exists(@"litery\" + nazwaPliku + krotnoscPliku + ".jpg")); // sprawdza czy plik o danej nazwie istnieje

                filename = @"litery\" + nazwaPliku + krotnoscPliku + ".jpg";
            }
            else
                filename = dlg.FileName;

            RenderTargetBitmap rtb =
            new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
            rtb.Render(MyCanvas);

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                File.SetAttributes(filename, FileAttributes.Normal);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);
                fs.Close();
            }

        }
    }
}
