using Emgu.CV;
using ImageMagick;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Buffer = Windows.Storage.Streams.Buffer;

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

        /*
        public static SoftwareBitmap ToSoftwareBitmap<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new ()
        {
            var swbmp = new SoftwareBitmap(BitmapPixelFormat.Bgra8, (int)image.Width, (int)image.Height);
            CvInvoke.cvGetRawData(image.Ptr, out var data, out var step, out var roiSize);

            unsafe
            {
                var size = (int)image.Width * (int)image.Height * 4;
                var bytes = new Span<byte>(data.ToPointer(), size);

                var buffer = Buffer.CreateCopyFromMemoryBuffer()
                

                swbmp.CopyFromBuffer(bytes);
            }

            return swbmp;
        }*/
    }
}
