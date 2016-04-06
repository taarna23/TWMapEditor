using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace MapEditor
{
   public static  class BitmapExtensions
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        public static System.Drawing.Bitmap BitmapSourceFromArray(byte[] pixels, int width, int height,int bitDept, BitmapPalette pal = null)
        {
            Graphics g = Graphics.FromHwnd(Process.GetCurrentProcess().MainWindowHandle);
            if (bitDept == 8)
            {
                return BitmapSourceFromArray(pixels, width, height, System.Windows.Media.PixelFormats.Indexed8, pal);
            }
            else if (bitDept == 16 )
            {
                return BitmapSourceFromArray(pixels, width, height, System.Windows.Media.PixelFormats.Bgr565, pal);
            }
            else if (bitDept == 24)
            {
                return BitmapSourceFromArray(pixels, width, height, System.Windows.Media.PixelFormats.Indexed8, pal);
            }
            else if (bitDept == 32)
            {
                return BitmapSourceFromArray(pixels, width, height, System.Windows.Media.PixelFormats.Indexed8, pal);
            }
            return null;
        }

        public static System.Drawing.Bitmap BitmapSourceFromArray(byte[] pixels, int width, int height, System.Windows.Media.PixelFormat pxF, BitmapPalette pal = null)
        {
            Graphics g = Graphics.FromHwnd(Process.GetCurrentProcess().MainWindowHandle);
            WriteableBitmap bitmap = new WriteableBitmap(width, height, g.DpiX, g.DpiY, pxF, pal);
            bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, width, height), pixels, width * (bitmap.Format.BitsPerPixel / 8), 0);
            return BitmapFromSource(bitmap);
        }

        public static bool Is16BitBmp(this Bitmap bitmap)
        {
            int _P = Image.GetPixelFormatSize(bitmap.PixelFormat);
            return (_P == 16);
        }

        public static System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        }

        public static Bitmap ConvertTo16bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            BitmapData bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int numbytes = bmpdata.Stride * bitmap.Height;
            byte[] bytedata = new byte[numbytes];
            IntPtr ptr = bmpdata.Scan0;

            Marshal.Copy(ptr, bytedata, 0, numbytes);

            bitmap.UnlockBits(bmpdata);

            return bytedata;
        }


        public static Bitmap RawArrayTo16Bpp(Byte [] Raw, Int32 Width, Int32 Height)
        {
            Bitmap b16bpp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
            var rect = new Rectangle(0, 0, Width, Height);
            var bitmapData = b16bpp.LockBits(rect, ImageLockMode.WriteOnly, b16bpp.PixelFormat);
            var numberOfBytes = bitmapData.Stride * Height;
            var ptr = bitmapData.Scan0;
            Marshal.Copy(Raw, 0, ptr, Raw.Length);
            b16bpp.UnlockBits(bitmapData);
            return b16bpp;
        }

        public static void RemovePadding(this Bitmap bitmap)
        {
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            var pixels = new byte[bitmapData.Width * bitmapData.Height * bytesPerPixel];

            for (int row = 0; row < bitmapData.Height; row++)
            {
                var dataBeginPointer = IntPtr.Add(bitmapData.Scan0, row * bitmapData.Stride);
                Marshal.Copy(dataBeginPointer, pixels, row * bitmapData.Width * bytesPerPixel, bitmapData.Width * bytesPerPixel);
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bitmap.UnlockBits(bitmapData);
        }

        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }

        private static Bitmap RotateImage(Bitmap rotateMe, float angle)
        {
            var bmp = new Bitmap(rotateMe.Width + (rotateMe.Width / 2), rotateMe.Height + (rotateMe.Height / 2));
            using (Graphics g = Graphics.FromImage(bmp))
                g.DrawImageUnscaled(rotateMe, (rotateMe.Width / 4), (rotateMe.Height / 4), bmp.Width, bmp.Height);

            rotateMe = bmp;
            Bitmap rotatedImage = new Bitmap(rotateMe.Width, rotateMe.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(rotateMe.Width / 2, rotateMe.Height / 2);   //set the rotation point as the center into the matrix
                g.RotateTransform(angle);                                        //rotate
                g.TranslateTransform(-rotateMe.Width / 2, -rotateMe.Height / 2); //restore rotation point into the matrix
                g.DrawImage(rotateMe, new Point(0, 0));                          //draw the image on the new bitmap
            }
            return rotatedImage;
        }

        public static Image PictureBoxZoom(Image imgP, Size size, int X, int Y)
        {
            Bitmap bm = new Bitmap(imgP, Convert.ToInt32(imgP.Width * size.Width), Convert.ToInt32(imgP.Height * size.Height));
            Graphics grap = Graphics.FromImage(bm);
            grap.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return bm;
        }

        public static Bitmap Transform(Bitmap source)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(source.Width, source.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            // create the negative color matrix
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
        new float[] {-1, 0, 0, 0, 0},
        new float[] {0, -1, 0, 0, 0},
        new float[] {0, 0, -1, 0, 0},
        new float[] {0, 0, 0, 1, 0},
        new float[] {1, 1, 1, 0, 1}
            });

            // create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height),
                        0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();

            return newBitmap;
        }
    }
}
