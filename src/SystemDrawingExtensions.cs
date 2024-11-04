using SkiaSharp;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static class SystemDrawingExtensions
    {
        public static SKBitmap ToSKBitmap(this Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);
                return SKBitmap.Decode(stream);
            }
        }

        public static SKBitmap ToSKBitmap(this Icon icon)
        {
            using (var stream = new MemoryStream())
            {
                icon.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return SKBitmap.Decode(stream);
            }
        }

        public static SKImage ToSKImage(this Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);
                return SKImage.FromEncodedData(stream);
            }
        }

        public static SKImage ToSKImage(this Icon icon)
        {
            using (var stream = new MemoryStream())
            {
                icon.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return SKImage.FromEncodedData(stream);
            }
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

        public static FormatConvertedBitmap ConvertPixelFormat(this BitmapSource source, System.Windows.Media.PixelFormat targetFormat)
        {
            var newFormatedBitmapSource = new FormatConvertedBitmap();

            // BitmapSource objects like FormatConvertedBitmap can only have their properties
            // changed within a BeginInit/EndInit block.
            newFormatedBitmapSource.BeginInit();

            // Use the BitmapSource object defined above as the source for this new
            // BitmapSource (chain the BitmapSource objects together).
            newFormatedBitmapSource.Source = source;

            // Set the new format to Gray32Float (grayscale).
            newFormatedBitmapSource.DestinationFormat = targetFormat;
            newFormatedBitmapSource.EndInit();

            return newFormatedBitmapSource;
        }
    }
}
