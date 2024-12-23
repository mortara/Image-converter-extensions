using Emgu.CV;
using Emgu.CV.Cuda;
using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ImageSharpExtensions
    {
        /// <summary>
        /// Converts an ImageSharp image to Microsoft.UI.Xaml.Media.Imaging.BitmapImage
        /// </summary>
        /// <typeparam name="TColor"></typeparam>
        /// <typeparam name="TDepth"></typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that BMP conversion step
        /// </ToDo>
        public static BitmapImage ToBitmapImage(this SixLabors.ImageSharp.Image img)
        {
            using(var ms = new MemoryStream())
            {
                var encoder = new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder();

                img.Save(ms, encoder);
                ms.Position = 0;
                var bitmapImage = new BitmapImage();
                using (var ras = ms.AsRandomAccessStream())
                    bitmapImage.SetSource(ras);

                return bitmapImage;
            }

        }

    }
}
