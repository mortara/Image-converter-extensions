using SkiaSharp;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SystemDrawingExtensions
    {
        /// <summary>
        /// Converts a System.Drawing.Icon to SKBitmap
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.SystemDrawingIcon, ImageLibraryFormat.SKBitmap, ConverterKind.Real)]
        public static SKBitmap ToSKBitmap(this Icon icon)
        {
            using (var stream = new MemoryStream())
            {
                icon.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return SKBitmap.Decode(stream);
            }
        }

        /// <summary>
        /// Converts a System.Drawing.Icon to SKImage
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.SystemDrawingIcon, ImageLibraryFormat.SKImage, ConverterKind.Real)]
        public static SKImage ToSKImage(this Icon icon)
        {

            using (var stream = new MemoryStream())
            {
                icon.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return SKImage.FromEncodedData(stream);
            }
        }
    }
}
