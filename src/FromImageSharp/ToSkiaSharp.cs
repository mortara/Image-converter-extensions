using SixLabors.ImageSharp;
using SkiaSharp;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ImageSharpExtensions
    {
        /// <summary>
        /// Converts ImageSharp.Image to SKImage
        /// </summary>
        /// <param name="ims"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SKImage, ConverterKind.Real)]
        public static SKImage ToSKImage(this Image ims)
        {
            using (var ms = new MemoryStream())
            {
                ims.SaveAsBmp(ms);
                ms.Position = 0;
                return SKImage.FromEncodedData(ms);
            }
        }

        /// <summary>
        /// Converts ImageSharp.Image to SKBitmap
        /// </summary>
        /// <param name="ims"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SKBitmap, ConverterKind.Real)]
        public static SKBitmap ToSKBitmap(this Image ims)
        {
            using (var ms = new MemoryStream())
            {
                ims.SaveAsBmp(ms);
                ms.Position = 0;
                return SKBitmap.Decode(ms);
            }
        }
    }
}
