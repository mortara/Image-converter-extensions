using SkiaSharp;
using System.Drawing.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts SKImage to System.Drawing.Bitmap with specified PixelFormat. Besides that, it's a copy of SKImage's own ToBitmap() method
        /// </summary>
        /// <param name="skiaImage"></param>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.SKImage, ImageLibraryFormat.SystemDrawingBitmap, ConverterKind.Real)]
        public static Bitmap ToBitmap(this SKImage skiaImage, PixelFormat pixelFormat = PixelFormat.Format32bppArgb)
        {
            var bitmap = new Bitmap(skiaImage.Width, skiaImage.Height, pixelFormat);
            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            using (var pixmap = new SKPixmap(new SKImageInfo(data.Width, data.Height), data.Scan0, data.Stride))
            {
                skiaImage.ReadPixels(pixmap, 0, 0);
            }

            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}
