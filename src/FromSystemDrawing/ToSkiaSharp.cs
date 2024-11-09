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
