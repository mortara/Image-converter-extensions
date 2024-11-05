using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using SkiaSharp;

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
    }
}
