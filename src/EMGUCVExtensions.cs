using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using SkiaSharp;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static  class EMGUCVExtensions
    {
        /// <summary>
        /// Converts Emgu.CV Image to SKImage
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        public static SKImage ToSKImage<TColor, TDepth>(this Emgu.CV.Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            return SKImage.FromBitmap(image.ToSKBitmap());
        }

        /// <summary>
        /// Converts Emgu.CV Image to SKBitmap
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        public static SKBitmap ToSKBitmap<TColor, TDepth>(this Emgu.CV.Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            Type typeFromHandle = typeof(TColor);
            var colorType = SKColorType.Unknown;
            var alphaType = SKAlphaType.Unknown;
            if (typeFromHandle == typeof(Gray))
            {
                colorType = SKColorType.Gray8;
                alphaType = SKAlphaType.Opaque;
            }
            else if (typeFromHandle == typeof(Bgra))
            {
                colorType = SKColorType.Bgra8888;
                alphaType = SKAlphaType.Premul;
            }
            else
            {
                using (var image2 = image.Convert<TColor, TDepth>())
                {
                    return image2.ToSKBitmap();
                }
            }

            var skbmp = new SKBitmap(image.Width, image.Height, colorType, alphaType);
            var pixmap = skbmp.PeekPixels();
            var pixelAddress = pixmap.GetPixels();
            var stride = skbmp.ColorType.GetBytesPerPixel() * image.Width;
            using (Mat m = new Mat(image.Size.Height, image.Size.Width, DepthType.Cv8U, image.NumberOfChannels, pixelAddress, stride))
            {
                image.Mat.CopyTo(m);
            }

            return skbmp;
        }

        /// <summary>
        /// Converts Emgu.CV image to Microsoft.UI.Xaml.Media.Imaging.BitmapImage
        /// </summary>
        /// <typeparam name="TColor"></typeparam>
        /// <typeparam name="TDepth"></typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of ToJpegData() call
        /// </ToDo>
        public static Microsoft.UI.Xaml.Media.Imaging.BitmapImage ToBitmapImage<TColor, TDepth>(this Emgu.CV.Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            var bitmapImage = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage();

            var bytes = image.ToJpegData();

            using (var imageStream = new MemoryStream())
            {
                imageStream.Write(bytes, 0, bytes.Length);
                imageStream.Seek(0, SeekOrigin.Begin);
                bitmapImage.SetSource(imageStream.AsRandomAccessStream());
            }

            return bitmapImage;

        }
    }
}
