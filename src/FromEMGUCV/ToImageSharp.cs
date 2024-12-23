using Emgu.CV;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class EMGUCVExtensions
    {
        /// <summary>
        /// Converts a EMGU.CV Image into a ImageSharp.Image. 
        /// </summary>
        /// <param name="mimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of JPEG re-encoding
        /// </ToDo>
        public static Image ToImageSharpImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            var jpegbytes = image.ToJpegData();

            return Image.Load(jpegbytes);
        }
    }
}
