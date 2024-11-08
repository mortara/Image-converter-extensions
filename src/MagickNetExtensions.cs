using Emgu.CV;
using ImageMagick;
using SkiaSharp;
using System.IO;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using System.Buffers;
using Windows.Storage.Streams;
using SkiaSharp.Views.Windows;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static  class MagickNetExtensions
    {
        /// <summary>
        /// Converts IMagickImage to SKImage
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        public static SKImage ToSKImage(this IMagickImage mimg)
        {
            using (var ms = new MemoryStream())
            {
                mimg.Write(ms, MagickFormat.Bmp3);
                ms.Position = 0;
                return SKImage.FromEncodedData(ms);
            }
        }

        /// <summary>
        /// Converts IMagickImage to SKBitmap
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        public static SKBitmap ToSKBitmap(this IMagickImage mimg)
        {
            using (var ms = new MemoryStream())
            {
                mimg.Write(ms, MagickFormat.Bmp3);
                ms.Position = 0;
                return SKBitmap.Decode(ms);
            }
        }

        /// <summary>
        /// Converts a IMagickIMage into a Microsoft.UI.Xaml.Media.Imaging.BitmapImage
        /// </summary>
        /// <param name="mimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        public static Microsoft.UI.Xaml.Media.Imaging.BitmapImage ToBitmapImage(this IMagickImage mimg)
        {
            var bitmapImage = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage();

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
