using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SystemDrawingExtensions
    {
        public static BitmapSource ToBitmapSource(this Bitmap bmp)
        {

            var bitmapData = bmp.LockBits(
              new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
              System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            var bitmapSource = BitmapSource.Create(
               bitmapData.Width, bitmapData.Height, 96, 96, PixelFormats.Bgr24, null,
               bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bmp.UnlockBits(bitmapData);
            return bitmapSource;
        }
    }
}
