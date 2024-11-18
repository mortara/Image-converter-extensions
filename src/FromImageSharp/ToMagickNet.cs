using ImageMagick;
using SixLabors.ImageSharp;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions.FromImageSharp
{
    public static partial class ImageSharpExtensions
    {
        public static IMagickImage ToMagickImage(this Image img)
        {
            using (var ms = new MemoryStream()) 
            {
                img.SaveAsBmp(ms);
                ms.Position = 0;
                return new MagickImage(ms, MagickFormat.Bmp3);
            }

        }
    }
}
