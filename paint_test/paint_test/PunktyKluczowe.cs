using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace paint_test
{
    class PunktyKluczowe
    {
        public static void punktyKluczowe()
        {
            double K = 0;
            Bitmap btmz = new Bitmap(@"savedimage.jpg");
            Bitmap btmF = new Bitmap(btmz.Width, btmz.Height);

            double[][] kernel = new double[3][];
            kernel[0] = new double[] { -1, -1, -1 };
            kernel[1] = new double[] { -1, 8, -1 };
            kernel[2] = new double[] { -1, -1, -1 };

            for (int i = 0; i < kernel.Length; i++)
            {
                for (int j = 0; j < kernel.Length; j++)
                {
                    K += kernel[i][j];
                }
            }

            if (K == 0)
                K = 1;

            for (int i = 0; i < btmz.Width - kernel.Length; i++)
            {
                for (int j = 0; j < btmz.Height - kernel[0].Length; j++)
                {
                    double convR = 0, convG = 0, convB = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            convR += 1 / K * kernel[k][l] * btmz.GetPixel(i - k + kernel.Length, j - l + kernel.Length).R;
                            convG += 1 / K * kernel[k][l] * btmz.GetPixel(i - k + kernel.Length, j - l + kernel.Length).G;
                            convB += 1 / K * kernel[k][l] * btmz.GetPixel(i - k + kernel.Length, j - l + kernel.Length).B;
                        }

                        if (convR < 0) convR = 0;
                        if (convG < 0) convG = 0;
                        if (convB < 0) convB = 0;
                    }
                    if (convR > 255) convR = 255;
                    if (convG > 255) convG = 255;
                    if (convB > 255) convB = 255;

                    Color c = Color.FromArgb((int)convR, (int)convG, (int)convB);

                    if (c.GetBrightness() == 0)          //Sprawdza czy dany punkt jest tym najciemniejszym
                        btmF.SetPixel(i, j, Color.FromArgb(240, 0, 0)); //jesli tak to go zamalowuje na czerwono (tutaj trzeba potem zmienic)
                    else
                        btmF.SetPixel(i, j, Color.FromArgb(btmz.GetPixel(i, j).R, btmz.GetPixel(i, j).G, btmz.GetPixel(i, j).B));

                }
            }
            btmF.Save(@"essa2.jpg");
        }
    }
}
