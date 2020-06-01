using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;

namespace ProyectoFinalArqHard
{
    class Program
    {
        static void version1(String ubicacion, int depth, int size)
        {
            //StreamWriter sw = new StreamWriter(@"C:\Users\juanm\OneDrive\Documentos\ICESI\7mo\Arquitectura de Computadores\Proyecto\time\Tiempo1.txt");
            Bitmap original = new Bitmap(ubicacion);
            //original.Save("C:/Users/juanm/OneDrive/Documentos/ICESI/7mo/Arquitectura de Computadores/Proyecto/img/Version1-0.png");
            Bitmap a = original;
            Color c;
            //Stopwatch
            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();
            timeA.Restart();
            timeA.Start();
            for (int i = 0; i < a.Height - 1; i++)
            {
                for(int j = 0; j < a.Width - 1; j++)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb((255 - c.R), (255 - c.G), (255 - c.B));
                    a.SetPixel(i, j, c);
                }
            }
            timeA.Stop();

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 1: " + tiempo);
            //sw.WriteLine("Version 1; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();

            a.Save("./inv/version1/" + depth + "/" + size + ".bmp");
        }

        static void version2(String ubicacion, int depth, int size)
        {
            //StreamWriter sw = new StreamWriter(@"C:\Users\juanm\OneDrive\Documentos\ICESI\7mo\Arquitectura de Computadores\Proyecto\time\Tiempo2.txt");
            Bitmap original = new Bitmap(ubicacion);
            //original.Save("C:/Users/juanm/OneDrive/Documentos/ICESI/7mo/Arquitectura de Computadores/Proyecto/Version2-0.png");
            Bitmap a = original;
            Color c;

            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();
            timeA.Restart();
            timeA.Start();
            for (int i = 0; i < a.Height - 1; i++)
            {
                for(int j = 0; j < a.Width - 1; j++)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb(255, (255 - c.R), c.G, c.B);
                    a.SetPixel(i, j, c);
                }
            }

            for (int i = 0; i < a.Height - 1; i++)
            {
                for (int j = 0; j < a.Width - 1; j++)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb(255, c.R, (255 - c.G), c.B);
                    a.SetPixel(i, j, c);
                }
            }

            for (int i = 0; i < a.Height - 1; i++)
            {
                for (int j = 0; j < a.Width - 1; j++)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb(255, c.R, c.G, (255 - c.B));
                    a.SetPixel(i, j, c);
                }
            }
            timeA.Stop();

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 2: " + tiempo);
            //sw.WriteLine("Version 2; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            a.Save("./inv/version2/" + depth + "/" + size + ".bmp");
        }

        static void version3(String ubicacion, int depth, int size)
        {
            //StreamWriter sw = new StreamWriter(@"C:\Users\juanm\OneDrive\Documentos\ICESI\7mo\Arquitectura de Computadores\Proyecto\time\Tiempo3.txt");
            Bitmap original = new Bitmap(ubicacion);
            //original.Save("C:/Users/juanm/OneDrive/Documentos/ICESI/7mo/Arquitectura de Computadores/Proyecto/Version3-0.png");
            Bitmap a = original;
            Color c;

            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();
            timeA.Restart();
            timeA.Start();
            for (int j = 0; j < a.Width - 1; j++)
            {
                for(int i = 0; i < a.Height - 1; i++)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb(255, (255 - c.R), (255 - c.G), (255 - c.B));
                    a.SetPixel(i, j, c);
                }
            }
            timeA.Stop();

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 3: " + tiempo);
            //sw.WriteLine("Version 3; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            a.Save("./inv/version3/" + depth + "/" + size + ".bmp");
        }

        static void version4(String ubicacion, int depth, int size)
        {
            //StreamWriter sw = new StreamWriter(@"C:\Users\juanm\OneDrive\Documentos\ICESI\7mo\Arquitectura de Computadores\Proyecto\time\Tiempo4.txt");
            Bitmap original = new Bitmap(ubicacion);
            //original.Save("C:/Users/juanm/OneDrive/Documentos/ICESI/7mo/Arquitectura de Computadores/Proyecto/Version4-0.png");
            Bitmap a = original;
            Color c;

            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();
            timeA.Restart();
            timeA.Start();
            for (int i = 0; i < a.Height - 1; i ++)
            {
                for (int j = 0; j < a.Width - 1; j++)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb(255, (255 - c.R), c.G, c.B);
                    a.SetPixel(i, j, c);
                }
            }

            for (int i = a.Height - 1; i > 0; i--)
            {
                for (int j = a.Width - 1; j > 0; j--)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb(255, c.R, (255 - c.G), (255 - c.B));
                    a.SetPixel(i, j, c);
                }
            }
            timeA.Stop();

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 4: " + tiempo);
            //sw.WriteLine("Version 4; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            a.Save("./inv/version4/" + depth + "/" + size + ".bmp");
        }

        static void version5(String ubicacion, int depth, int size)
        {
            //StreamWriter sw = new StreamWriter(@"C:\Users\juanm\OneDrive\Documentos\ICESI\7mo\Arquitectura de Computadores\Proyecto\time\Tiempo5.txt");
            Bitmap original = new Bitmap(ubicacion);
            //original.Save("C:/Users/juanm/OneDrive/Documentos/ICESI/7mo/Arquitectura de Computadores/Proyecto/Version5-0.png");
            Bitmap a = original;
            Color c;

            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();
            timeA.Restart();
            timeA.Start();
            for (int i = 0; i < a.Height - 2; i += 2)
            {
                for(int j = 0; j < a.Width - 2; j += 2)
                {
                    c = a.GetPixel(i, j);
                    c = Color.FromArgb(255, (255 - c.R), (255 - c.G), (255 - c.B));
                    a.SetPixel(i, j, c);

                    c = a.GetPixel(i, j + 1);
                    c = Color.FromArgb(255, (255 - c.R), (255 - c.G), (255 - c.B));
                    a.SetPixel(i, j + 1, c);

                    c = a.GetPixel(i + 1, j);
                    c = Color.FromArgb(255, (255 - c.R), (255 - c.G), (255 - c.B));
                    a.SetPixel(i + 1, j, c);

                    c = a.GetPixel(i + 1, j + 1);
                    c = Color.FromArgb(255, (255 - c.R), (255 - c.G), (255 - c.B));
                    a.SetPixel(i + 1, j + 1, c);
                }
            }
            timeA.Stop();

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 5: " + tiempo);
            //sw.WriteLine("Version 5; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            a.Save("./inv/version5/" + depth + "/" + size + ".bmp");
        }
       

        static void Main(string[] args)
        {
            Console.WriteLine("Digite la version");
            int version = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite el tamaño de la imagen");
            int size = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite la profundidad en bits de la imagen");
            int depth = int.Parse(Console.ReadLine());

            String imagen = "./img/" + size + "(" + depth + ").bmp";
            for (int i = 0; i < 3; i++)
            {
                try
                {

                    switch (version)
                    {
                        case 1:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, depth, size);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, depth, size);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, depth, size);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, depth, size);
                                }

                            }
                            break;

                        case 2:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, depth, size);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, depth, size);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, depth, size);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, depth, size);
                                }

                            }
                            break;

                        case 3:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, depth, size);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, depth, size);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, depth, size);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, depth, size);
                                }

                            }
                            break;

                        case 4:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, depth, size);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, depth, size);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, depth, size);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, depth, size);
                                }

                            }
                            break;

                        case 5:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, depth, size);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, depth, size);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, depth, size);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, depth, size);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, depth, size);
                                }

                            }
                            break;

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
