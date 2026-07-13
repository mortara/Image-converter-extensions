using ImageMagick;
using SkiaSharp;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class MagickNetExtensions
    {
        /// <summary>
        /// Converts IMagickImage to SKImage
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.MagickImage, ImageLibraryFormat.SKImage, ConverterKind.Real)]
        public static SKImage ToSKImage(this IMagickImage mimg)
        {
            using (var ms = new MemoryStream())
            {
                mimg.Write(ms, MagickFormat.Bmp3);
                ms.Position = 0;
                return SKImage.FromEncodedData(ms);
            }
        }

        /// <summary>
        /// Converts IMagickImage to SKBitmap
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.MagickImage, ImageLibraryFormat.SKBitmap, ConverterKind.Real)]
        public static SKBitmap ToSKBitmap(this IMagickImage mimg)
        {
            using (var ms = new MemoryStream())
            {
                mimg.Write(ms, MagickFormat.Bmp3);
                ms.Position = 0;
                return SKBitmap.Decode(ms);
            }
        }
    }
}
