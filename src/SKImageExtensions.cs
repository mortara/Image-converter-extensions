using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using ImageMagick.Factories;
using SkiaSharp;
using System.Drawing.Imaging;
using System.IO;


namespace PMortara.Helpers.ImageConverterExtensions
{
    public static class SKImageExtensions
    {
       
        public static IMagickImage ToMagickImage(this SKImage skimg)
        { 
            var bmp = skimg.ToBitmap(PixelFormat.Format32bppArgb);
            try
            {
                var f = new MagickFactory();
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Bmp);
                    ms.Position = 0;
                    return new MagickImage(f.Image.Create(ms));
                }
            }
            finally
            {
                bmp.Dispose();
            }
        }

        public static Image<Bgra, byte> ToEMGUImage(this SKImage img)
        {
            var bmp = img.ToBitmap(PixelFormat.Format32bppArgb);

            try
            {          
                return BitmapExtension.ToImage<Bgra, byte>(bmp);
            }
            finally
            {
                bmp.Dispose();
            }
        }

        /// <summary>
        /// Converts SKImage to System.Drawing.Bitmap with specified PixelFormat. Besides that, it's a copy of SKImage's own ToBitmap() method
        /// </summary>
        /// <param name="skiaImage"></param>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap ToBitmap(this SKImage skiaImage, System.Drawing.Imaging.PixelFormat pixelFormat)
        {
            var bitmap = new System.Drawing.Bitmap(skiaImage.Width, skiaImage.Height, pixelFormat);
            var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bitmap.PixelFormat);

            using (var pixmap = new SKPixmap(new SKImageInfo(data.Width, data.Height), data.Scan0, data.Stride))
            {
                skiaImage.ReadPixels(pixmap, 0, 0);
            }

            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}
