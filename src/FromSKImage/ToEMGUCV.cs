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
        [ImageConverter("1.0", "2024-11-05", ImageLibraryFormat.SKImage, ImageLibraryFormat.EMGUCVImage, ConverterKind.Real,
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

        [ImageConverter("2.0", "2026-07-13", ImageLibraryFormat.SKImage, ImageLibraryFormat.EMGUCVImage, ConverterKind.Real,
            GenericArguments = new[] { typeof(Bgra), typeof(byte) })]
        public static Image<TColor, TDepth> ToEMGUImage_V2<TColor, TDepth>(this SKImage img)
            where TColor : struct, IColor
            where TDepth : new()
        {
            // Aktuell nur für Bgra/byte sinnvoll
            if (typeof(TColor) != typeof(Emgu.CV.Structure.Bgra) || typeof(TDepth) != typeof(byte))
                throw new NotSupportedException("ToEMGUImage_V2 unterstützt aktuell nur Bgra/byte.");
            
            using var pixmap = img.PeekPixels();
            if (pixmap == null)
                throw new InvalidOperationException("SKImage has no pixel data.");

            int width = pixmap.Width;
            int height = pixmap.Height;
            int stride = pixmap.RowBytes;

            // Emgu erwartet BGRA, SkiaSharp liefert BGRA bei SKColorType.Bgra8888
            var emguImage = new Image<Emgu.CV.Structure.Bgra, byte>(width, height);

            unsafe
            {
                byte* srcPtr = (byte*)pixmap.GetPixels().ToPointer();
                byte* dstPtr = (byte*)emguImage.MIplImage.ImageData.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    Buffer.MemoryCopy(srcPtr + y * stride, dstPtr + y * emguImage.MIplImage.WidthStep, emguImage.MIplImage.WidthStep, width * 4);
                }
            }

            return emguImage as Image<TColor, TDepth>;
        }
    }
}
