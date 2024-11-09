using Emgu.CV;
using ImageMagick;
using ImageMagick.Factories;
using PMortara.Helpers.ImageConverterExtensions;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Drawing.Imaging;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKBitmapExtensions
    {
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
                return bmp.ToImage<TColor, TDepth>();
            }
            finally
            {
                bmp?.Dispose();
            }
        }
    }
}
