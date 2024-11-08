using Emgu.CV;
using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp;
using System.Drawing.Imaging;
using System.IO;


namespace PMortara.Helpers.ImageConverterExtensions
{
    public static class SystemDrawingExtensions
    {
        /// <summary>
        /// Converts a System.Drawing.Icon to SKBitmap
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static SKBitmap ToSKBitmap(this Icon icon)
        {
            using (var stream = new MemoryStream())
            {
                icon.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return SKBitmap.Decode(stream);
            }
        }

        /// <summary>
        /// Converts a System.Drawing.Icon to SKImage
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static SKImage ToSKImage(this Icon icon)
        {

            using (var stream = new MemoryStream())
            {
                icon.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return SKImage.FromEncodedData(stream);
            }
        }

        /// <summary>
        /// Converts a System.Drawing.Bitmap to a Microsoft.UI.Xaml.Media.Imaging.BitmapImage        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Microsoft.UI.Xaml.Media.Imaging.BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            var bitmapImage = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage();
          
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
            var bitmapImage = new WriteableBitmap(bitmap.Width,bitmap.Height);

            var format = ImageFormat.Bmp;

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, format);
                stream.Seek(0, SeekOrigin.Begin);
                bitmapImage.SetSource(stream.AsRandomAccessStream());
            }

            return bitmapImage;

        }

        public static Bitmap ConvertPixelFormat(this Bitmap source, PixelFormat targetFormat)
        {
            var bmpnew = new Bitmap(source.Width, source.Height, targetFormat);
            using (var g = Graphics.FromImage(bmpnew))
            {
                g.DrawImage(source, new PointF(0, 0));
            }

            return bmpnew;
        }

       
    }
}
