using ImageMagick;
using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp.Views.Windows;
using System.IO;
using Windows.Graphics.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class MagickNetExtensions
    {
        /// <summary>
        /// Converts a IMagickIMage into a Microsoft.UI.Xaml.Media.Imaging.BitmapImage
        /// </summary>
        /// <param name="mimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        public static BitmapImage ToBitmapImage(this IMagickImage mimg)
        {
            var bitmapImage = new BitmapImage();

            using (var ms = new MemoryStream())
            {
                mimg.Write(ms, MagickFormat.Bmp3);
                ms.Position = 0;
                bitmapImage.SetSource(ms.AsRandomAccessStream());
            }

            return bitmapImage;

        }

        public static WriteableBitmap ToWriteableBitmap(this IMagickImage magickimage)
        {
            return magickimage.ToSKBitmap().ToWriteableBitmap();
        }

        
    }
}
