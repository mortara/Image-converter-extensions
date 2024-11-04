using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using ImageMagick.Factories;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.UI.Xaml.Media.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static class SKBitmapExtensions
    {
        public static IMagickImage ToMagickImage(this SKBitmap skbmp)
        {
            var bmp = skbmp.ToBitmap();
            try
            {
                var f = new MagickFactory();
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Bmp);
                    ms.Position = 0;
                    return new MagickImage(f.Image.Create(ms));
                }
            }
            finally
            {
                bmp?.Dispose();
            }
        }

        public static Image<Bgra, byte> ToEMGUImage(this SKBitmap skbmp)
        {
            var bmp = skbmp.ToBitmap();

            try
            {
                if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    var bmpnew = bmp.ConvertPixelFormat(PixelFormat.Format32bppArgb);        
                    bmp.Dispose();
                    bmp = bmpnew;
                }

                return BitmapExtension.ToImage<Bgra, byte>(bmp);
            }
            finally
            {
                bmp?.Dispose();
            }
        }

        public static BitmapSource ToBitmapSource(this SKBitmap img)
        {
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

        public static BitmapImage ToBitmapImage(this SKBitmap img)
        {    
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
