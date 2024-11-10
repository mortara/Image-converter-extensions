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
        /// Improve handling of different pixelformats
        /// </ToDo>
        public static IMagickImage ToMagickImage(this SKBitmap skbmp)
        {
            var pixels = skbmp.GetPixelSpan();
            var settings = new MagickReadSettings();
            settings.Width = (uint)skbmp.Width;
            settings.Height = (uint)skbmp.Height;
         
            switch (skbmp.ColorType)
            {
                case SKColorType.Rgba8888:
                    settings.Depth = 8;
                    settings.Format = MagickFormat.Rgba;
                    break;
                case SKColorType.Bgra8888:
                    settings.Depth = 8;
                    settings.Format = MagickFormat.Bgra;
                    break;
            }
            
            return new MagickImage(pixels, settings);
          
        }
    }
}
