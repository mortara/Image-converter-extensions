using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions.FromSKBitmap
{
    public static partial class SKBitmapExtensions
    {     
        /// <summary>
        /// Converts a SKBitmap into a ImageSharp.Image. 
        /// </summary>
        /// <param name="skiaBitmap"></param>
        /// <returns></returns>
        public static Image ToImageSharpImage(this SKBitmap skiaBitmap)
        {
            /// Directy load Pixeldata for known ColorTypes
            if (skiaBitmap.ColorType == SKColorType.Rgba8888)
                return Image.LoadPixelData<Rgba32>(skiaBitmap.GetPixelSpan(), skiaBitmap.Width, skiaBitmap.Height);

            if (skiaBitmap.ColorType == SKColorType.Bgra8888)
                return Image.LoadPixelData<Bgra32>(skiaBitmap.GetPixelSpan(), skiaBitmap.Width, skiaBitmap.Height);

            if (skiaBitmap.ColorType == SKColorType.Gray8)
                return Image.LoadPixelData<L8>(skiaBitmap.GetPixelSpan(), skiaBitmap.Width, skiaBitmap.Height);


            // All other Colortypes are processed pixel by pixel
            var image = new Image<Rgba32>(skiaBitmap.Width, skiaBitmap.Height);
            for (int y = 0; y < skiaBitmap.Height; y++)
            {
                for (int x = 0; x < skiaBitmap.Width; x++)
                {
                    var color = skiaBitmap.GetPixel(x, y);
                    image[x, y] = new Rgba32(color.Red, color.Green, color.Blue, color.Alpha);
                }
            }

            return image;
        }
    }
}
