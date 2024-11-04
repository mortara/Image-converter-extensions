using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using ImageMagick.Factories;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing.Imaging;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static class SKBitmapExtensions
    {
        public static IMagickImage ToMagickImage(this SKBitmap skbmp)
        {
            var bmp = skbmp.ToBitmap();
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
                bmp?.Dispose();
            }
        }

        public static Image<Bgra, byte> ToEMGUImage(this SKBitmap skbmp)
        {
            var bmp = skbmp.ToBitmap(PixelFormat.Format32bppArgb);

            try
            {
              
                return BitmapExtension.ToImage<Bgra, byte>(bmp);
            }
            finally
            {
                bmp?.Dispose();
            }
        }


        public static System.Drawing.Bitmap ToBitmap(this SKBitmap skiaBitmap, System.Drawing.Imaging.PixelFormat pixelFormat)
        {
            using (var pixmap = skiaBitmap.PeekPixels())
            using (var image = SKImage.FromPixels(pixmap))
            {
                var bmp = image.ToBitmap(pixelFormat);
                GC.KeepAlive(skiaBitmap);
                return bmp;
            }
        }
    }
}
