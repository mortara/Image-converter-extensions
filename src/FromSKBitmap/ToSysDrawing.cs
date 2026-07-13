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
        [ImageConverter("1.0", "2024-11-05", ImageLibraryFormat.SKBitmap, ImageLibraryFormat.SystemDrawingBitmap, ConverterKind.Real)]
        public static Bitmap ToBitmap(this SKBitmap skiaBitmap, PixelFormat pixelFormat = PixelFormat.Format32bppArgb)
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

        [ImageConverter("1.0", "2024-11-10", ImageLibraryFormat.SKBitmap, ImageLibraryFormat.SystemDrawingBitmap, ConverterKind.InMemoryWrapper)]
        public static Bitmap AsBitmap(this SKBitmap skiaBitmap)
        {
            var stride = skiaBitmap.ColorType.GetBytesPerPixel() * skiaBitmap.Width;

            var pixformat = PixelFormat.Format32bppArgb;

            return new Bitmap(skiaBitmap.Width, skiaBitmap.Height, stride, pixformat, skiaBitmap.GetPixels());
        }
    }
}
