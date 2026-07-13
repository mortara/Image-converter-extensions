using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.UI.Xaml.Media.Imaging;

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
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.EMGUCVImage, ImageLibraryFormat.WinUIWriteableBitmap, ConverterKind.Real,
            GenericArguments = new[] { typeof(Bgra), typeof(byte) })]
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
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.EMGUCVImage, ImageLibraryFormat.WinUIBitmapImage, ConverterKind.Real,
            GenericArguments = new[] { typeof(Bgra), typeof(byte) })]
        public static BitmapImage ToBitmapImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            return image.AsBitmap().ToBitmapImage();
        }

    }
}
