using ImageMagick;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions.FromMagickNET
{
    public static partial class MagickNetExtensions
    {
        public static Image ToImageSharpImage(this IMagickImage mimg)
        {
            using (var ms = new MemoryStream())
            {
                
                mimg.Write(ms, MagickFormat.Bmp3);
                ms.Position = 0;
                return Image.Load(ms);
            }
        }
    }
}
