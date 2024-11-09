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
