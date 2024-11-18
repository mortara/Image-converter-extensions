using SixLabors.ImageSharp;
using SkiaSharp;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions.FromSKBitmap
{
    public static partial class SKBitmapExtensions
    {
        /// <summary>
        /// Converts a SKBitmap into a ImageSharp.Image. 
        /// </summary>
        /// <param name="mimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of PNG re-encoding
        /// </ToDo>
        public static Image ToImageSharpImage(this SKBitmap skiaBitmap)
        {
            using (var ms = new MemoryStream())
            {
                skiaBitmap.Encode(ms, SKEncodedImageFormat.Png, 100);
                ms.Position = 0;

                return Image.Load(ms);

            }
        }
    }
}
