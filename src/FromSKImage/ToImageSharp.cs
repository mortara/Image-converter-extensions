using SixLabors.ImageSharp;
using SkiaSharp;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions.FromSKBitmap
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts a SKImage into a ImageSharp.Image. 
        /// </summary>
        /// <param name="mimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of PNG re-encoding
        /// </ToDo>
        public static Image ToImageSharpImage(this SKImage skiaImage)
        {
            using (var skdata = skiaImage.Encode(SKEncodedImageFormat.Png, 100))
            {
                return Image.Load(skdata.Span);
            }
        }
    }
}
