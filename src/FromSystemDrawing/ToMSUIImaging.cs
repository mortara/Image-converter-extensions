using Microsoft.UI.Xaml.Media.Imaging;
using System.Drawing.Imaging;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SystemDrawingExtensions
    {
        /// <summary>
        /// Converts a System.Drawing.Bitmap to a Microsoft.UI.Xaml.Media.Imaging.BitmapImage        
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            var bitmapImage = new BitmapImage();

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                using (var ras = stream.AsRandomAccessStream())
                    bitmapImage.SetSource(ras);
            }

            return bitmapImage;

        }

        public static WriteableBitmap ToWriteableBitmap(this Bitmap bitmap)
        {
            var bitmapImage = new WriteableBitmap(bitmap.Width, bitmap.Height);

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);
                using(var ras = stream.AsRandomAccessStream())
                    bitmapImage.SetSource(ras);
            }

            return bitmapImage;

        }

        
    }
}
