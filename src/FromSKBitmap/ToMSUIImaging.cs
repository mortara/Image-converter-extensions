using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKBitmapExtensions
    {
        /// <summary>
        /// Converts a SKBitmap into a Microsoft.UI.Xaml.Media.Imaging.BitmapImage. Kind of useless, since SKBitmap has it's own ToWriteableBitmap()
        /// </summary>
        /// <param name="mimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of re-encoding
        /// </ToDo>
        public static Microsoft.UI.Xaml.Media.Imaging.BitmapImage ToBitmapImage(this SKBitmap skiaBitmap)
        {

            var bitmapImage = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage();

            using (var ms = new MemoryStream())
            {
                skiaBitmap.Encode(ms, SKEncodedImageFormat.Png, 100);
                ms.Position = 0;
                bitmapImage.SetSource(ms.AsRandomAccessStream());
            }

            return bitmapImage;

        }
    }
}
