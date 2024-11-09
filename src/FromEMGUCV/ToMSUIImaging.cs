using Emgu.CV;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class EMGUCVExtensions
    {
        public static Microsoft.UI.Xaml.Media.Imaging.WriteableBitmap ToWriteableBitmap<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
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
        /// Get rid of that ToBitmap() step
        /// </ToDo>
        public static Microsoft.UI.Xaml.Media.Imaging.BitmapImage ToBitmapImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            return image.AsBitmap().ToBitmapImage();
        }
    }
}
