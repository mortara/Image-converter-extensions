using Emgu.CV;
using Emgu.CV.CvEnum;
using Microsoft.UI.Xaml.Media.Imaging;
using OpenTK.Compute.OpenCL;
using SkiaSharp;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class EMGUCVExtensions
    {
        /// <summary>
        /// Converts Emgu.CV image to Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap
        /// </summary>
        /// <typeparam name="TColor"></typeparam>
        /// <typeparam name="TDepth"></typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that AsBitmap() step
        /// </ToDo>
        public static WriteableBitmap ToWriteableBitmap<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            return image.AsBitmap().ToWriteableBitmap();
        }

        /// <summary>
        /// Converts Emgu.CV image to Microsoft.UI.Xaml.Media.Imaging.BitmapImage
        /// </summary>
        /// <typeparam name="TColor"></typeparam>
        /// <typeparam name="TDepth"></typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that AsBitmap() step
        /// </ToDo>
        public static BitmapImage ToBitmapImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            return image.AsBitmap().ToBitmapImage();
        }

    }
}
