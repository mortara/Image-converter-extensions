using CommunityToolkit.HighPerformance;
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Windows.Media.Media3D;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ImageSharpExtensions
    {
        public static Image<TColor, TDepth> ToEMGUImage<TColor, TDepth>(this Image<Rgb24> ims) where TColor : struct, IColor where TDepth : new()
        {
            return ims.AsEMGUCVImage().Convert<TColor, TDepth>();

        }

        /// <summary>
        /// Converts ImageSharp Image to Emgu.CV Image
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// 
        /// </ToDo>
        public static Image<TColor, TDepth> ToEMGUImage_v2<TColor, TDepth>(this Image<Rgb24> ims) where TColor : struct, IColor where TDepth : new()
        {
            using (var target = new Image<Rgb, byte>(ims.Width, ims.Height))
            {
                ims.CopyPixelDataTo(target.Data.AsSpan<byte>());
                return target.Convert<TColor, TDepth>();
            }
        }


        /// <summary>
        /// Converts IMageSharp Image to Emgu.CV Image
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that ToBitmap() step.
        /// </ToDo>
        public static Image<TColor, TDepth> ToEMGUImage_v1<TColor, TDepth>(this Image ims) where TColor : struct, IColor where TDepth : new()
        {
           
            try
            {
                using (var ms = new MemoryStream())
                {
                    ims.SaveAsBmp(ms);
                    ms.Position = 0;

                    var imageFromBytes = new Image<TColor, TDepth>(ims.Width, ims.Height);

                    CvInvoke.Imdecode(ms.ToArray(), Emgu.CV.CvEnum.ImreadModes.AnyColor, imageFromBytes.Mat);

                    return imageFromBytes;
                }

            }
            finally
            {
               
            }
        }


        /// <summary>
        /// Wraps an ImageSharp Rgb24 image as a EMGUCV Image without copying the pixel data
        /// 
        /// </summary>
        /// <param name="ims"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Check for memory-leaks etc
        /// </ToDo>
        public unsafe static Image<Rgb, byte> AsEMGUCVImage(this Image<Rgb24> ims)
        {
            var stride = ims.PixelType.BitsPerPixel / 8 * ims.Width;

            ims.DangerousTryGetSinglePixelMemory(out var memoryManager);

            var scan0 = (nint)memoryManager.Pin().Pointer;

            var image = new Image<Rgb, byte>(ims.Width, ims.Height, stride, scan0);

            return image;

        }

        /// <summary>
        /// Wraps an ImageSharp Bgr24 image as a EMGUCV Image without copying the pixel data
        /// 
        /// </summary>
        /// <param name="ims"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Check for memory-leaks etc
        /// </ToDo>
        public unsafe static Image<Bgr, byte> AsEMGUCVImage(this Image<Bgr24> ims)
        {
            var stride = ims.PixelType.BitsPerPixel / 8 * ims.Width;

            ims.DangerousTryGetSinglePixelMemory(out var memoryManager);

            var scan0 = (nint)memoryManager.Pin().Pointer;

            var image = new Image<Bgr, byte>(ims.Width, ims.Height, stride, scan0);

            return image;

        }
    }
}
