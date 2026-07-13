using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ImageSharpExtensions
    {
        [ImageConverter("1.0", "2024-11-18", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SystemDrawingBitmap, ConverterKind.Real)]
        public static System.Drawing.Image ToArray(this SixLabors.ImageSharp.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, PngFormat.Instance);
                ms.Position=0;
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                return returnImage;
            }
        }



    }
}
