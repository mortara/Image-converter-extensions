using Emgu.CV;
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

        /// <summary>
        /// Converts SKBitmap to IMagickImage
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that ToBitmap() step.
        /// </ToDo>
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

        /// <summary>
        /// Converts SKBitmap to Emgu.CV Image
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that ToBitmap() step.
        /// </ToDo>
        public static Image<TColor, TDepth> ToEMGUImage<TColor, TDepth>(this SKBitmap skbmp) where TColor : struct, IColor where TDepth : new()
        {
            var bmp = skbmp.ToBitmap(PixelFormat.Format32bppArgb);

            try
            {
              
                return BitmapExtension.ToImage<TColor, TDepth>(bmp);
            }
            finally
            {
                bmp?.Dispose();
            }
        }

        /// <summary>
        /// Converts SKBitmap to System.Drawing.Bitmap with specified PixelFormat. Besides that, it's a copy of SKBitmap's own ToBitmap() method
        /// </summary>
        /// <param name="skiaBitmap"></param>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
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
