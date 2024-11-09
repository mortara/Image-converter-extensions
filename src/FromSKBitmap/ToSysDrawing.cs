using SkiaSharp;
using System.Drawing.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKBitmapExtensions
    {
        /// <summary>
        /// Converts SKBitmap to System.Drawing.Bitmap with specified PixelFormat. Besides that, it's a copy of SKBitmap's own ToBitmap() method
        /// </summary>
        /// <param name="skiaBitmap"></param>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this SKBitmap skiaBitmap, PixelFormat pixelFormat)
        {
            using (var pixmap = skiaBitmap.PeekPixels())
            {
                using (var image = SKImage.FromPixels(pixmap))
                {
                    var bmp = image.ToBitmap(pixelFormat);
                    GC.KeepAlive(skiaBitmap);
                    return bmp;
                }
            }
        }
    }
}
