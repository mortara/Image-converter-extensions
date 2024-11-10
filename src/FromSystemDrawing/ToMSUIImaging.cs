using Microsoft.UI.Xaml.Media.Imaging;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SystemDrawingExtensions
    {
        /// <summary>
        /// Converts a System.Drawing.Bitmap to a Microsoft.UI.Xaml.Media.Imaging.BitmapImage        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            var bitmapImage = new BitmapImage();

            var format = ImageFormat.Bmp;

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, format);
                stream.Seek(0, SeekOrigin.Begin);
                bitmapImage.SetSource(stream.AsRandomAccessStream());
            }

            return bitmapImage;

        }

        public static WriteableBitmap ToWriteableBitmap(this Bitmap bitmap)
        {
            var bitmapImage = new WriteableBitmap(bitmap.Width, bitmap.Height);

            var format = ImageFormat.Bmp;

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, format);
                stream.Seek(0, SeekOrigin.Begin);
                bitmapImage.SetSource(stream.AsRandomAccessStream());
            }

            return bitmapImage;

        }

        
    }
}
