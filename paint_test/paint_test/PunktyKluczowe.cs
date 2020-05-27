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
using System.Windows;

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

                for (int Xcount = 0; Xcount < btm.Width / 2; Xcount++)
                {
                    for (int Ycount = 0; Ycount < btm.Height / 2; Ycount++)
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

                if ((sumaCzarnych > sumaCzarnychOryginal * 1.05 || sumaCzarnych < sumaCzarnychOryginal * 0.65) && sumaCzarnych > 5000)
                    return wynik = 0;


                for (int Xcount = btm.Width / 2; Xcount < btm.Width; Xcount++)
                {
                    for (int Ycount = btm.Height / 2; Ycount < btm.Height; Ycount++)
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

                if ((sumaCzarnych < sumaCzarnychOryginal * 1.05 && sumaCzarnych >= sumaCzarnychOryginal * 0.65) || sumaCzarnych < 5000)
                {
                    //iloscCzarnych zróżnicowanie
                    if (iloscCzarnych >= sumaCzarnych * 0.8) iloscCzarnych += iloscCzarnych * 0.05;
                    else if (iloscCzarnych < sumaCzarnych * 0.8 && iloscCzarnych > sumaCzarnych * 0.75) iloscCzarnych += iloscCzarnych * 0.08;
                    else if (iloscCzarnych <= sumaCzarnych * 0.75 && iloscCzarnych > sumaCzarnych * 0.7) iloscCzarnych += iloscCzarnych * 0.12;
                    else if (iloscCzarnych <= sumaCzarnych * 0.7 && iloscCzarnych > sumaCzarnych * 0.65) iloscCzarnych += iloscCzarnych * 0.15;
                    else if (iloscCzarnych <= sumaCzarnych * 0.65 && iloscCzarnych > sumaCzarnych * 0.6) iloscCzarnych += iloscCzarnych * 0.14;
                    else if (iloscCzarnych <= sumaCzarnych * 0.6 && iloscCzarnych > sumaCzarnych * 0.55) iloscCzarnych += iloscCzarnych * 0.12;
                    else if (iloscCzarnych <= sumaCzarnych * 0.55 && iloscCzarnych > sumaCzarnych * 0.5) iloscCzarnych += iloscCzarnych * 0.08;
                    else if (iloscCzarnych <= sumaCzarnych * 0.5 && iloscCzarnych > sumaCzarnych * 0.45) iloscCzarnych -= iloscCzarnych * 0.02;
                    else if (iloscCzarnych <= sumaCzarnych * 0.45 && iloscCzarnych > sumaCzarnych * 0.4) iloscCzarnych -= iloscCzarnych * 0.03;
                    else if (iloscCzarnych <= sumaCzarnych * 0.4 && iloscCzarnych > sumaCzarnych * 0.35) iloscCzarnych -= iloscCzarnych * 0.06;
                    else if (iloscCzarnych <= sumaCzarnych * 0.35 && iloscCzarnych > sumaCzarnych * 0.3) iloscCzarnych -= iloscCzarnych * 0.07;
                    else if (iloscCzarnych <= sumaCzarnych * 0.3)
                    {
                        iloscCzarnych -= iloscCzarnych * 0.05;
                        sumaCzarnych += sumaCzarnych * 0.1;
                    }

                    //iloscPrzeciwnych zróżnicowanie
                    if (iloscPrzeciwnych <= sumaCzarnych * 0.2) iloscPrzeciwnych /= 6;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.2 && iloscPrzeciwnych <= sumaCzarnych * 0.25) iloscPrzeciwnych /= 6;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.25 && iloscPrzeciwnych <= sumaCzarnych * 0.3) iloscPrzeciwnych /= 5 - iloscPrzeciwnych * 0.02;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.3 && iloscPrzeciwnych <= sumaCzarnych * 0.35) iloscPrzeciwnych /= 4 + iloscPrzeciwnych * 0.05;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.35 && iloscPrzeciwnych <= sumaCzarnych * 0.4) iloscPrzeciwnych /= 3.5 + iloscPrzeciwnych * 0.05;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.4 && iloscPrzeciwnych <= sumaCzarnych * 0.45) iloscPrzeciwnych /= 3 + iloscPrzeciwnych * 0.1;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.45 && iloscPrzeciwnych <= sumaCzarnych * 0.5) iloscPrzeciwnych /= 2.5 + iloscPrzeciwnych * 0.1;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.5 && iloscPrzeciwnych <= sumaCzarnych * 0.55) iloscPrzeciwnych /= 2 + iloscPrzeciwnych * 0.15;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.55 && iloscPrzeciwnych <= sumaCzarnych * 0.6) iloscPrzeciwnych /= 1.5 + iloscPrzeciwnych * 0.15;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.6 && iloscPrzeciwnych <= sumaCzarnych * 0.65) iloscPrzeciwnych += (iloscPrzeciwnych * 0.15) / 1.5;
                    else if (iloscPrzeciwnych > sumaCzarnych * 0.65)
                    {
                        iloscPrzeciwnych += (iloscPrzeciwnych * 0.2) / 2;
                        sumaCzarnych += sumaCzarnych * 0.1;
                    }

                    wynik = (iloscCzarnych - iloscPrzeciwnych) / sumaCzarnych;
                }

                else
                    wynik = 0;


                wynik *= 100;
                return (Math.Round(wynik, 2));

            }
        }

    }
}
