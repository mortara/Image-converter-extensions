using ImageMagick;
using Imageflow.Fluent;
using PMortara.Helpers.ImageConverterExtensions;
using PMortara.Helpers.ImageConverterExtensions.FromByteArray;
using SkiaSharp;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using Image = SixLabors.ImageSharp.Image;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace TestAndBenchmark
{
    /// <summary>
    /// Stamps "TEST!!" centered onto a converter's result, right after the
    /// converter runs and before the result is turned into a displayable
    /// WinUI image -- this proves the converted object can still be
    /// manipulated with an appropriate library's own drawing API.
    /// </summary>
    public static class TestOverlay
    {
        private const string OverlayText = "TEST!!";

        /// <summary>
        /// Draws the overlay onto the result and returns a ready-to-display
        /// System.Drawing.Bitmap, or null if this TargetFormat has no drawable
        /// representation available here (caller should then display the
        /// un-stamped result instead).
        /// </summary>
        public static async Task<Bitmap> StampAsync(object result, ImageLibraryFormat targetFormat)
        {
            var bitmap = await ToDrawableBitmapAsync(result, targetFormat);
            if (bitmap is null)
                return null;

            using (var g = Graphics.FromImage(bitmap))
            using (var font = new Font("Arial", System.Math.Max(12f, bitmap.Width / 15f), FontStyle.Bold))
            {
                var textSize = g.MeasureString(OverlayText, font);
                var origin = new PointF(
                    (bitmap.Width - textSize.Width) / 2f,
                    (bitmap.Height - textSize.Height) / 2f);
                g.DrawString(OverlayText, font, Brushes.Red, origin);
            }

            return bitmap;
        }

        private static async Task<Bitmap> ToDrawableBitmapAsync(object result, ImageLibraryFormat targetFormat)
        {
            switch (targetFormat)
            {
                case ImageLibraryFormat.SKImage:
                    return ((SKImage)result).ToBitmap(PixelFormat.Format32bppArgb);

                case ImageLibraryFormat.SKBitmap:
                    return ((SKBitmap)result).ToBitmap(PixelFormat.Format32bppArgb);

                case ImageLibraryFormat.SystemDrawingBitmap:
                    return (Bitmap)result;

                case ImageLibraryFormat.ByteArray:
                    return (Bitmap)((byte[])result).ToDrawingImage();

                case ImageLibraryFormat.ImageSharpImage:
                    return (Bitmap)((Image)result).ToArray();

                case ImageLibraryFormat.MagickImage:
                    using (var skImage = ((IMagickImage)result).ToSKImage())
                        return skImage.ToBitmap(PixelFormat.Format32bppArgb);

                case ImageLibraryFormat.EMGUCVImage:
                    return EmguResultToBitmap(result);

                case ImageLibraryFormat.ImageFlowBuildNode:
                    using (var skImage = await ((BuildNode)result).ToSKImageAsync())
                        return skImage.ToBitmap(PixelFormat.Format32bppArgb);

                default:
                    // WPF BitmapSource, WinUI-native BitmapImage/WriteableBitmap: no
                    // drawable conversion wired up here; leave the result unmodified.
                    return null;
            }
        }

        private static Bitmap EmguResultToBitmap(object emguImage)
        {
            // Reuse our own EMGU -> ImageSharp -> System.Drawing chain (both
            // already-attributed converters) instead of guessing at the
            // declaring type of Emgu.CV.Bitmap's external ToBitmap() extension.
            var imageType = emguImage.GetType();
            var typeArguments = imageType.GetGenericArguments();
            var toImageSharp = typeof(EMGUCVExtensions)
                .GetMethod(nameof(EMGUCVExtensions.ToImageSharpImage), BindingFlags.Public | BindingFlags.Static)!
                .MakeGenericMethod(typeArguments);

            var imageSharpImage = (Image)toImageSharp.Invoke(null, new[] { emguImage })!;
            return (Bitmap)imageSharpImage.ToArray();
        }
    }
}
