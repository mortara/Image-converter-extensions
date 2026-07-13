using ImageMagick;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions.FromMagickNET
{
    public static partial class MagickNetExtensions
    {
        [ImageConverter("1.0", "2024-11-18", ImageLibraryFormat.MagickImage, ImageLibraryFormat.ImageSharpImage, ConverterKind.Real)]
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
