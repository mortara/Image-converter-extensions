using ImageMagick;
using SkiaSharp;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static  class MagickNetExtensions
    {
        public static SKImage ToSKImage(this IMagickImage mimg)
        {
            using (var ms = new MemoryStream())
            {
                mimg.Write(ms, MagickFormat.Bmp3);
                ms.Position = 0;
                return SKImage.FromEncodedData(ms);
            }
        }

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
