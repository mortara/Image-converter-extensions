using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using ImageMagick.Factories;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing;
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
            var bmp = skbmp.ToBitmap();

            try
            {
                if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    var bmpnew = bmp.ConvertPixelFormat(PixelFormat.Format32bppArgb);        
                    bmp.Dispose();
                    bmp = bmpnew;
                }

                return BitmapExtension.ToImage<Bgra, byte>(bmp);
            }
            finally
            {
                bmp?.Dispose();
            }
        }
    }
}
