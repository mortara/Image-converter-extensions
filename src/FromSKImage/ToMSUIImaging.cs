using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts a SKImage to a BitmapImage. Kind of useless, since SKImage has it's own ToWriteableBitmap()
        /// </summary>
        /// <param name="skiaImage"></param>
        /// <returns></returns>
        public static Microsoft.UI.Xaml.Media.Imaging.BitmapImage ToBitmapImage(this SKImage skiaImage)
        {
            var bitmapImage = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage();

            var encoded = skiaImage.Encode();
            using (var ms = new MemoryStream())
            {
                encoded.SaveTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                bitmapImage.SetSource(ms.AsRandomAccessStream());
            }

            return bitmapImage;

        }
    }
}
