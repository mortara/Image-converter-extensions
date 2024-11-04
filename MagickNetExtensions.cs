using ImageMagick;
using SkiaSharp;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static  class MagickNetExtensions
    {
        public static SKImage ToSKImage(this IMagickImage mimg)
        {
            var fmt = MagickFormat.Bmp3;

            using (var ms = new MemoryStream())
            {
                mimg.Write(ms, fmt);
                ms.Position = 0;
                return SKImage.FromEncodedData(ms);
            }
        }
    }
}
