using ImageMagick;
using ImageMagick.Factories;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing.Imaging;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKBitmapExtensions
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
    }
}
