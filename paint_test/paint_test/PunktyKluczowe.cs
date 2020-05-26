using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Threading;

namespace paint_test
{
    class PunktyKluczowe : MainWindow
    {
        public PunktyKluczowe()
        {
        }

        public double porownajZBaza(string nazwaPlikuZBazy)
        {

            using (Bitmap btm = new Bitmap(@"savedImage.jpg"))
            {

                Bitmap btmF = new Bitmap(nazwaPlikuZBazy);
                Bitmap btmTest = new Bitmap(btm.Width, btm.Height);

                double iloscCzarnych = 0;
                double iloscPrzeciwnych = 0;
                double sumaCzarnych = 0;
                double sumaCzarnychOryginal = 0;
                double wynik = 0;

                for (int Xcount = 0; Xcount < btm.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < btm.Height; Ycount++)
                    {

                        if (((btm.GetPixel(Xcount, Ycount).R == 0) && (btm.GetPixel(Xcount, Ycount).G == 0) && (btm.GetPixel(Xcount, Ycount).B == 0)) && ((btmF.GetPixel(Xcount, Ycount).R == 255) && (btmF.GetPixel(Xcount, Ycount).G == 255) && (btmF.GetPixel(Xcount, Ycount).B == 255)))
                            iloscPrzeciwnych++; //narysowany czarny, z bazy bialy


                        if (((btm.GetPixel(Xcount, Ycount).R == 0) && (btm.GetPixel(Xcount, Ycount).G == 0) && (btm.GetPixel(Xcount, Ycount).B == 0)) && ((btmF.GetPixel(Xcount, Ycount).R == 0) && (btmF.GetPixel(Xcount, Ycount).G == 0) && (btmF.GetPixel(Xcount, Ycount).B == 0)))
                            iloscCzarnych++; // narysowany czarny, z bazy czarny


                        if ((btm.GetPixel(Xcount, Ycount).R == 0) && (btm.GetPixel(Xcount, Ycount).G == 0) && (btm.GetPixel(Xcount, Ycount).B == 0))
                            sumaCzarnych++;

                        if ((btmF.GetPixel(Xcount, Ycount).R == 0) && (btmF.GetPixel(Xcount, Ycount).G == 0) && (btmF.GetPixel(Xcount, Ycount).B == 0))
                            sumaCzarnychOryginal++;
                    }
                }


                if (sumaCzarnych >= sumaCzarnychOryginal * 0.95)
                {
                    //iloscCzarnych zróżnicowanie
                    if (iloscCzarnych >= sumaCzarnych * 0.8) iloscCzarnych += iloscCzarnych * 0.05;
                    else if (iloscCzarnych < sumaCzarnych * 0.8 && iloscCzarnych > sumaCzarnych * 0.75) iloscCzarnych += iloscCzarnych * 0.08;
                    else if (iloscCzarnych <= sumaCzarnych * 0.75 && iloscCzarnych > sumaCzarnych * 0.7) iloscCzarnych += iloscCzarnych * 0.12;
                    else if (iloscCzarnych <= sumaCzarnych * 0.7 && iloscCzarnych > sumaCzarnych * 0.65) iloscCzarnych += iloscCzarnych * 0.15;
                    else if (iloscCzarnych <= sumaCzarnych * 0.65 && iloscCzarnych > sumaCzarnych * 0.6) iloscCzarnych += iloscCzarnych * 0.18;
                    else if (iloscCzarnych <= sumaCzarnych * 0.6 && iloscCzarnych > sumaCzarnych * 0.55) iloscCzarnych += iloscCzarnych * 0.2;
                    else iloscCzarnych += iloscCzarnych * 0.25;

                    //iloscPrzeciwnych zróżnicowanie
                    if (iloscPrzeciwnych <= sumaCzarnych * 0.2) iloscPrzeciwnych /= 10 + iloscPrzeciwnych * 0.1;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.2 && iloscPrzeciwnych <= sumaCzarnych * 0.25) iloscPrzeciwnych /= 9 + iloscPrzeciwnych * 0.12;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.25 && iloscPrzeciwnych <= sumaCzarnych * 0.3) iloscPrzeciwnych /= 8 + iloscPrzeciwnych * 0.14;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.3 && iloscPrzeciwnych <= sumaCzarnych * 0.35) iloscPrzeciwnych /= 7 + iloscPrzeciwnych * 0.16;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.35 && iloscPrzeciwnych <= sumaCzarnych * 0.4) iloscPrzeciwnych /= 6 + iloscPrzeciwnych * 0.18;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.4 && iloscPrzeciwnych <= sumaCzarnych * 0.45) iloscPrzeciwnych /= 5 + iloscPrzeciwnych * 0.2;
                    else iloscPrzeciwnych /= 4 + iloscPrzeciwnych * 0.2;

                    wynik = (iloscCzarnych - iloscPrzeciwnych) / sumaCzarnych;
                }

                else
                    wynik = 0;

                if (sumaCzarnych < 6000)
                    wynik = 0;

            }
        }

    }
}
