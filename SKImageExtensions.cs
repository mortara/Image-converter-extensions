using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using ImageMagick.Factories;
using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Windows;
using System.Drawing;
using System.Drawing.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static class SKImageExtensions
    {
        public static IMagickImage ToMagickImage(this SKImage skimg)
        {
            IMagickImage img = null;
            var bmp = skimg.ToBitmap();
            var f = new MagickFactory();
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Bmp);
                ms.Position = 0;
                img = new MagickImage(f.Image.Create(ms));
            }
            return img;
        }

        public static Image<Bgra, byte> ToEMGUImage(this SKImage img)
        {
            var bmp = img.ToBitmap();

            if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
            {
                var bmpnew = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

                using (var g = Graphics.FromImage(bmpnew))
                {
                    g.DrawImage(bmp, new PointF(0, 0));
                }

                bmp.Dispose();
                bmp = bmpnew;
            }

            var emgu = BitmapExtension.ToImage<Bgra, byte>(bmp);
            bmp.Dispose();
            return emgu;

        }

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

        public static BitmapSource ToBitmapSource(this SKImage img)
        {
            img = img.ToRasterImage();
            using (var pixels = img.PeekPixels())
            {
                if (pixels == null)
                    return img.ToWriteableBitmap();
                var filters = SKPngEncoderFilterFlags.NoFilters;
                int compress = 0;
                var options = new SKPngEncoderOptions(filters, compress);
                using (var data = pixels.Encode(options))
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.CreateOptions = BitmapCreateOptions.None;
                    bitmapImage.SetSource(data.AsStream().AsRandomAccessStream());
                    return bitmapImage;
                }
            }
        }

        public static BitmapImage ToBitmapImage(this SKImage img)
        {
            img = img.ToRasterImage();
            using (var pixels = img.PeekPixels())
            {
                var filters = SKPngEncoderFilterFlags.NoFilters;
                int compress = 0;
                var options = new SKPngEncoderOptions(filters, compress);
                using (var data = pixels.Encode(options))
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.CreateOptions = BitmapCreateOptions.None;
                    bitmapImage.SetSource(data.AsStream().AsRandomAccessStream());
                    return bitmapImage;
                }
            }
        }
    }
}
