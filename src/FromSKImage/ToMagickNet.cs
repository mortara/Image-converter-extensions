using ImageMagick;
using ImageMagick.Factories;
using SkiaSharp;
using System.Drawing.Imaging;
using System.IO;


namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts SKImage to IMagickImage
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that FromImage() step.
        /// </ToDo>
        public static IMagickImage ToMagickImage(this SKImage skimg)
        {
            var bmp = SKBitmap.FromImage(skimg);
            
            return bmp.ToMagickImage();

        }

    }
}
