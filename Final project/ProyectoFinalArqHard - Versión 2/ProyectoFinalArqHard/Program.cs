using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ProyectoFinalArqHard
{
    class ColorRGB
    {
        public ushort Red8 { get; set; }
        public ushort Green8 { get; set; }
        public ushort Blue8 { get; set; }

        public UInt32 Red { get; set; }
        public UInt32 Green { get; set; }
        public UInt32 Blue { get; set; }

        public ColorRGB(ushort red, ushort green, ushort blue)
        {
            Red8 = red;
            Green8 = green;
            Blue8 = blue;
        }

        public ColorRGB(UInt32 red, UInt32 green, UInt32 blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

    }

    class Program
    {
        private static ColorRGB[,] convertirMatrizShort(List<UInt64> red, List<UInt64> green, List<UInt64> blue, int n)
        {
            ColorRGB[,] matriz = new ColorRGB[n,n];
            int count = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    matriz[i,j] = new ColorRGB((ushort)red[count], (ushort)green[count], (ushort)blue[count]);
                    count++;
                }
                
            }
            return matriz;
        }

        private static ColorRGB[,] convertirMatriz32(List<UInt64> red, List<UInt64> green, List<UInt64> blue, int n)
        {
            ColorRGB[,] matriz = new ColorRGB[n, n];
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matriz[i, j] = new ColorRGB((UInt32)red[count], (UInt32)green[count], (UInt32)blue[count]);
                    count++;
                }

            }
            return matriz;
        }

        private static List<UInt64> GetRedValuesWithLockBits(Bitmap bitmap)
        {
            List<UInt64> result = new List<UInt64>();
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0,0,bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            try
            {
                unsafe
                {
                    byte* ppixelRow = (byte*)bitmapData.Scan0;

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        ushort* ppixelData = (ushort*)ppixelRow;

                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // components are stored in BGR order, i.e. red component last
                            result.Add(ppixelData[2]);
                            ppixelData += 3;
                        }

                        ppixelRow += bitmapData.Stride;
                    }
                }

                return result;
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        private static List<UInt64> GetGreenValuesWithLockBits(Bitmap bitmap)
        {
            List<UInt64> result = new List<UInt64>();
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            try
            {
                unsafe
                {
                    byte* ppixelRow = (byte*)bitmapData.Scan0;

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        ushort* ppixelData = (ushort*)ppixelRow;

                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // components are stored in BGR order, i.e. red component last
                            result.Add(ppixelData[1]);
                            ppixelData += 3;
                        }

                        ppixelRow += bitmapData.Stride;
                    }
                }

                return result;
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        private static List<UInt64> GetBlueValuesWithLockBits(Bitmap bitmap)
        {
            List<UInt64> result = new List<UInt64>();
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            try
            {
                unsafe
                {
                    byte* ppixelRow = (byte*)bitmapData.Scan0;

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        ushort* ppixelData = (ushort*)ppixelRow;

                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // components are stored in BGR order, i.e. red component last
                            result.Add(ppixelData[0]);
                            ppixelData += 3;
                        }

                        ppixelRow += bitmapData.Stride;
                    }
                }

                return result;
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
        
        static void version1(String ubicacion, PixelFormat depth, int size, int prof)
        {
            //StreamWriter sw = new StreamWriter(@"C:\Users\juanm\OneDrive\Documentos\ICESI\7mo\Arquitectura de Computadores\Proyecto\time\Tiempo1.txt");
            Bitmap original = new Bitmap(ubicacion);
            
            Bitmap a = (Bitmap) original.Clone(new Rectangle(0,0,original.Width,original.Height), depth);
            original.Dispose();
            
            ColorRGB[,] c = new ColorRGB[size,size];
            //Stopwatch
            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();

            List<UInt64> red = GetRedValuesWithLockBits(a);
            List<UInt64> green = GetGreenValuesWithLockBits(a);
            List<UInt64> blue = GetBlueValuesWithLockBits(a);
            ushort n8 = 0;
            UInt32 n = 0;
            if(prof == 16)
            {
                n8 = 65535;
                c = convertirMatrizShort(red, green, blue, size);
            } 
            if(prof == 24)
            {
                n = 16777215;
                c = convertirMatriz32(red, green, blue, size);
            } 
            if(prof == 32)
            {
                n = 4294967295;
                c = convertirMatriz32(red, green, blue, size);
            }
            if(prof == 16)
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        Console.WriteLine(c[i,j].Red8 + " " + c[i, j].Green8 + " " + c[i, j].Blue8);
                        c[i, j].Red8 = (ushort)(n8 - c[i, j].Red8);
                        c[i, j].Green8 = (ushort)(n8 - c[i, j].Green8);
                        c[i, j].Blue8 = (ushort)(n8 - c[i, j].Blue8);
                        Console.WriteLine(c[i, j].Red8 + " " + c[i, j].Green8 + " " + c[i, j].Blue8);
                    }
                }
                timeA.Stop();
            }
            else
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Red = (UInt32)(n - c[i, j].Red);//Eso no se guarda 
                        c[i, j].Green = (UInt32)(n - c[i, j].Green);
                        c[i, j].Blue = (UInt32)(n - c[i, j].Blue);
                    }
                }
                timeA.Stop();
            }
            

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 1: " + tiempo);

            //a.Save("./inv/version1/" + prof + "/" + size + ".bmp");
        }

        static void version2(String ubicacion, PixelFormat depth, int size, int prof)
        {
            Bitmap original = new Bitmap(ubicacion);

            Bitmap a = (Bitmap)original.Clone(new Rectangle(0, 0, original.Width, original.Height), depth);
            original.Dispose();

            ColorRGB[,] c = new ColorRGB[size, size];
            //Stopwatch
            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();

            List<UInt64> red = GetRedValuesWithLockBits(a);
            List<UInt64> green = GetGreenValuesWithLockBits(a);
            List<UInt64> blue = GetBlueValuesWithLockBits(a);
            ushort n8 = 0;
            UInt32 n = 0;
            if (prof == 16)
            {
                n8 = 65535;
                c = convertirMatrizShort(red, green, blue, size);
            }
            if (prof == 24)
            {
                n = 16777215;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 32)
            {
                n = 4294967295;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 16)
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Red8 = (ushort)(n8 - c[i, j].Red8);


                    }
                }

                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Green8 = (ushort)(n8 - c[i, j].Green8);
                    }
                }

                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Blue8 = (ushort)(n8 - c[i, j].Blue8);
                    }
                }
                timeA.Stop();
            }
            else
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Red = (UInt32)(n - c[i, j].Red);
                    }
                }

                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Green = (UInt32)(n - c[i, j].Green);
                    }
                }

                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Blue = (UInt32)(n - c[i, j].Blue);
                    }
                }
                timeA.Stop();
            }

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 2: " + tiempo);
            //sw.WriteLine("Version 2; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            //a.Save("./inv/version2/" + depth + "/" + size + ".bmp");
        }

        static void version3(String ubicacion, PixelFormat depth, int size, int prof)
        {
            Bitmap original = new Bitmap(ubicacion);

            Bitmap a = (Bitmap)original.Clone(new Rectangle(0, 0, original.Width, original.Height), depth);
            original.Dispose();

            ColorRGB[,] c = new ColorRGB[size, size];
            //Stopwatch
            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();

            List<UInt64> red = GetRedValuesWithLockBits(a);
            List<UInt64> green = GetGreenValuesWithLockBits(a);
            List<UInt64> blue = GetBlueValuesWithLockBits(a);
            ushort n8 = 0;
            UInt32 n = 0;
            if (prof == 16)
            {
                n8 = 65535;
                c = convertirMatrizShort(red, green, blue, size);
            }
            if (prof == 24)
            {
                n = 16777215;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 32)
            {
                n = 4294967295;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 16)
            {
                timeA.Restart();
                timeA.Start();
                for (int j = 0; j < a.Height; j++)
                {
                    for (int i = 0; i < a.Width; i++)
                    {
                        c[i, j].Red8 = (ushort)(n8 - c[i, j].Red8);
                        c[i, j].Green8 = (ushort)(n8 - c[i, j].Green8);
                        c[i, j].Blue8 = (ushort)(n8 - c[i, j].Blue8);
                    }
                }
                timeA.Stop();
            }
            else
            {
                timeA.Restart();
                timeA.Start();
                for (int j = 0; j < a.Height; j++)
                {
                    for (int i = 0; i < a.Width; i++)
                    {
                        c[i, j].Red = (UInt32)(n - c[i, j].Red);//ESTAN CARGANDO, tomorrow funcionan
                        c[i, j].Green = (UInt32)(n - c[i, j].Green);
                        c[i, j].Blue = (UInt32)(n - c[i, j].Blue);
                    }
                }
                timeA.Stop();
            }

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 3: " + tiempo);
            //sw.WriteLine("Version 3; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            //a.Save("./inv/version3/" + depth + "/" + size + ".bmp");
        }

        static void version4(String ubicacion, PixelFormat depth, int size, int prof)
        {
            Bitmap original = new Bitmap(ubicacion);

            Bitmap a = (Bitmap)original.Clone(new Rectangle(0, 0, original.Width, original.Height), depth);
            original.Dispose();

            ColorRGB[,] c = new ColorRGB[size, size];
            //Stopwatch
            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();

            List<UInt64> red = GetRedValuesWithLockBits(a);
            List<UInt64> green = GetGreenValuesWithLockBits(a);
            List<UInt64> blue = GetBlueValuesWithLockBits(a);
            ushort n8 = 0;
            UInt32 n = 0;
            if (prof == 16)
            {
                n8 = 65535;
                c = convertirMatrizShort(red, green, blue, size);
            }
            if (prof == 24)
            {
                n = 16777215;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 32)
            {
                n = 4294967295;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 16)
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Red8 = (ushort)(n8 - c[i, j].Red8);
                    }
                }

                for (int i = a.Height - 1; i > 0; i--)
                {
                    for (int j = a.Width - 1; j > 0; j--)
                    {
                        c[i, j].Green8 = (ushort)(n8 - c[i, j].Green8);
                        c[i, j].Blue8 = (ushort)(n8 - c[i, j].Blue8);
                    }
                }
                timeA.Stop();
            }
            else
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height; i++)
                {
                    for (int j = 0; j < a.Width; j++)
                    {
                        c[i, j].Red = (UInt32)(n - c[i, j].Red);
                    }
                }

                for (int i = a.Height; i > 0; i--)
                {
                    for (int j = a.Width; j > 0; j--)
                    {
                        c[i, j].Green = (UInt32)(n - c[i, j].Green);
                        c[i, j].Blue = (UInt32)(n - c[i, j].Blue);
                    }
                }
                timeA.Stop();
            }
            

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 4: " + tiempo);
            //sw.WriteLine("Version 4; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            //a.Save("./inv/version4/" + depth + "/" + size + ".bmp");
        }

        static void version5(String ubicacion, PixelFormat depth, int size, int prof)
        {
            Bitmap original = new Bitmap(ubicacion);

            Bitmap a = (Bitmap)original.Clone(new Rectangle(0, 0, original.Width, original.Height), depth);
            original.Dispose();

            ColorRGB[,] c = new ColorRGB[size, size];
            //Stopwatch
            long tiempo = 0;
            Stopwatch timeA = new Stopwatch();

            List<UInt64> red = GetRedValuesWithLockBits(a);
            List<UInt64> green = GetGreenValuesWithLockBits(a);
            List<UInt64> blue = GetBlueValuesWithLockBits(a);
            ushort n8 = 0;
            UInt32 n = 0;
            if (prof == 16)
            {
                n8 = 65535;
                c = convertirMatrizShort(red, green, blue, size);
            }
            if (prof == 24)
            {
                n = 16777215;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 32)
            {
                n = 4294967295;
                c = convertirMatriz32(red, green, blue, size);
            }
            if (prof == 16)
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height - 1; i += 2)
                {
                    for (int j = 0; j < a.Width - 1; j += 2)
                    {
                        c[i, j].Red8 = (ushort)(n8 - c[i, j].Red8);
                        c[i, j].Green8 = (ushort)(n8 - c[i, j].Green8);
                        c[i, j].Blue8 = (ushort)(n8 - c[i, j].Blue8);

                        c[i, j + 1].Red8 = (ushort)(n8 - c[i, j + 1].Red8);
                        c[i, j + 1].Green8 = (ushort)(n8 - c[i, j + 1].Green8);
                        c[i, j + 1].Blue8 = (ushort)(n8 - c[i, j + 1].Blue8);

                        c[i + 1, j].Red8 = (ushort)(n8 - c[i + 1, j].Red8);
                        c[i + 1, j].Green8 = (ushort)(n8 - c[i + 1, j].Green8);
                        c[i + 1, j].Blue8 = (ushort)(n8 - c[i + 1, j].Blue8);

                        c[i + 1, j + 1].Red8 = (ushort)(n8 - c[i + 1, j + 1].Red8);
                        c[i + 1, j + 1].Green8 = (ushort)(n8 - c[i + 1, j + 1].Green8);
                        c[i + 1, j + 1].Blue8 = (ushort)(n8 - c[i + 1, j + 1].Blue8);
                    }
                }
                timeA.Stop();
            }
            else
            {
                timeA.Restart();
                timeA.Start();
                for (int i = 0; i < a.Height - 1; i += 2)
                {
                    for (int j = 0; j < a.Width - 1; j += 2)
                    {
                        c[i, j].Red = (UInt32)(n - c[i, j].Red);
                        c[i, j].Green = (UInt32)(n - c[i, j].Green);
                        c[i, j].Blue = (UInt32)(n - c[i, j].Blue);

                        c[i, j + 1].Red = (UInt32)(n - c[i, j + 1].Red);
                        c[i, j + 1].Green = (UInt32)(n - c[i, j + 1].Green);
                        c[i, j + 1].Blue = (UInt32)(n - c[i, j + 1].Blue);

                        c[i + 1, j].Red = (UInt32)(n - c[i + 1, j].Red);
                        c[i + 1, j].Green = (UInt32)(n - c[i + 1, j].Green);
                        c[i + 1, j].Blue = (UInt32)(n - c[i + 1, j].Blue);

                        c[i + 1, j + 1].Red = (UInt32)(n - c[i + 1, j + 1].Red);
                        c[i + 1, j + 1].Green = (UInt32)(n - c[i + 1, j + 1].Green);
                        c[i + 1, j + 1].Blue = (UInt32)(n - c[i + 1, j + 1].Blue);
                    }
                }
                timeA.Stop();
            }
            

            tiempo = (long)(timeA.Elapsed.TotalMilliseconds * 1000000); //*1000000 ns; *1000 us
            Console.WriteLine("Version 5: " + tiempo);
            //sw.WriteLine("Version 5; " + a.Height + "x" + a.Width + "; " + tiempo);
            //sw.Close();
            //a.Save("./inv/version5/" + depth + "/" + size + ".bmp");
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
            for (int i = 0; i < 1; i++)
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
                                    
                                    version1(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {

                                    version1(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {

                                    version1(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {

                                    version1(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version1(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version1(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            break;

                        case 2:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version2(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version2(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version2(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            break;

                        case 3:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version3(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version3(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version3(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            break;

                        case 4:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version4(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version4(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version4(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            break;

                        case 5:
                            if (size == 64)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 160)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 512)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, PixelFormat.Format24bppRgb, size, depth);
                                }

                            }
                            if (size == 1500)
                            {
                                if (depth == 16)
                                {
                                    version5(imagen, PixelFormat.Format16bppRgb565, size, depth);
                                }
                                if (depth == 32)
                                {
                                    version5(imagen, PixelFormat.Format32bppRgb, size, depth);
                                }
                                if (depth == 24)
                                {
                                    version5(imagen, PixelFormat.Format24bppRgb, size, depth);
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
