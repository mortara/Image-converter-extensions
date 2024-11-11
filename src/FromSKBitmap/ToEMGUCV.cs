using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Windows.Themes;
using SkiaSharp;
using System.Drawing.Imaging;

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
            var bmp = skbmp.AsBitmap();

            try
            {
                return bmp.ToImage<TColor, TDepth>();
            }
            finally
            {
               
            }
        }

        /// <summary>
        /// Creates an EMGUCV Image from a SKBitmap without copying the pixel data
        /// 
        /// </summary>
        /// <param name="skbmp"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Check for memory-leaks etc
        /// </ToDo>
        public static Image<Bgra, byte> AsEMGUCVImage(this SKBitmap skbmp)
        {
            var stride = skbmp.ColorType.GetBytesPerPixel() * skbmp.Width;
            var scan0 = skbmp.GetPixels();

            var image = new Image<Bgra, byte>(skbmp.Width, skbmp.Height, stride, scan0);

            return image;

        }
    }
}
