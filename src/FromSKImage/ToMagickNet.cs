using Emgu.CV;
using ImageMagick;
using ImageMagick.Factories;
using SkiaSharp;
using System.Drawing.Imaging;
using System.IO;
using SkiaSharp.Views.Windows;


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
        /// Get rid of that ToBitmap() step.
        /// </ToDo>
        public static IMagickImage ToMagickImage(this SKImage skimg)
        {
            var bmp = skimg.ToBitmap(PixelFormat.Format32bppArgb);
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
                bmp.Dispose();
            }
        } 
    }
}
