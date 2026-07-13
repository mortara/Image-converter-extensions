using Emgu.CV;
using Emgu.CV.Structure;
using SkiaSharp;
using System.Drawing.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts SKImage to Emgu.CV Image
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that ToBitmap() step.
        /// </ToDo>
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.SKImage, ImageLibraryFormat.EMGUCVImage, ConverterKind.Real,
            GenericArguments = new[] { typeof(Bgra), typeof(byte) })]
        public static Image<TColor, TDepth> ToEMGUImage<TColor, TDepth>(this SKImage img) where TColor : struct, IColor where TDepth : new()
        {
            var bmp = img.ToBitmap(PixelFormat.Format32bppArgb);

            try
            {
                return bmp.ToImage<TColor, TDepth>();
            }
            finally
            {
                bmp.Dispose();
            }
        }

    }
}
