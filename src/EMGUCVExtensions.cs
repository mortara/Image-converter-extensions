using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ImageMagick;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Windows.Media.Imaging;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using System.Windows.Media.Media3D;

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
            Type depthFromHandle = typeof(TDepth);
            var colorType = SKColorType.Unknown;
            var alphaType = SKAlphaType.Unknown;
            if (typeFromHandle == typeof(Gray))
            {
                colorType = SKColorType.Gray8;   
                alphaType = SKAlphaType.Opaque;
            }
            else if (typeFromHandle == typeof(Bgra))
            {
                if(depthFromHandle == typeof(byte))
                    colorType = SKColorType.Bgra8888;

                alphaType = SKAlphaType.Premul;
            }
            else
            {
                using (var image2 = image.Convert<Bgra, byte>())
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
        /// Get rid of that ToBitmap() step
        /// </ToDo>
        public static Microsoft.UI.Xaml.Media.Imaging.BitmapImage ToBitmapImage<TColor, TDepth>(this Emgu.CV.Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            return image.AsBitmap().ToBitmapImage();
        }


        public static Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap ToWriteableBitmap<TColor, TDepth>(this Emgu.CV.Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            return image.AsBitmap().ToWriteableBitmap();
        }

        public static System.Windows.Media.Imaging.BitmapSource ToWPFBitmapImage<TColor, TDepth>(this Emgu.CV.Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            var bmp = image.ToBitmap();
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmp.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
        }

    }
}
