using SkiaSharp;
using System.IO;
using Microsoft.UI.Xaml.Media.Imaging;
using Imageflow.Fluent;
using System.Security.Cryptography;

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
        /// Get rid of PNG re-encoding
        /// </ToDo>
        public static BitmapImage ToBitmapImage(this SKBitmap skiaBitmap)
        {
            using (var pixmap = skiaBitmap.PeekPixels())
            {
                var filters = SKPngEncoderFilterFlags.NoFilters;
                int compress = 0;
                var options = new SKPngEncoderOptions(filters, compress);
                using (var data = pixmap.Encode(options))
                {
                    var bitmapImage = new BitmapImage();
                    using(var stream = data.AsStream())
                        using (var ras = stream.AsRandomAccessStream())
                            bitmapImage.SetSource(ras);
                    return bitmapImage;
                }
            }
        }


    }
}
