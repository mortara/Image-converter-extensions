using ImageMagick;
using Imageflow.Fluent;
using Microsoft.UI.Xaml.Media.Imaging;
using PMortara.Helpers.ImageConverterExtensions;
using SkiaSharp;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Image = SixLabors.ImageSharp.Image;
using SysBitmap = System.Drawing.Bitmap;

namespace TestAndBenchmark
{
    /// <summary>
    /// Converts an arbitrary converter result object into a WinUI BitmapSource for
    /// display, dispatching on the converter's declared TargetFormat. Reuses the
    /// library's own To*/As* extension methods instead of duplicating conversion
    /// logic.
    /// </summary>
    public static class ResultDisplayAdapter
    {
        public static async Task<BitmapSource> ToDisplayImageAsync(object result, ImageLibraryFormat targetFormat)
        {
            if (result is null)
                return null;

            switch (targetFormat)
            {
                case ImageLibraryFormat.WinUIBitmapImage:
                case ImageLibraryFormat.WinUIWriteableBitmap:
                    return (BitmapSource)result;

                case ImageLibraryFormat.SKImage:
                    return ((SKImage)result).ToBitmapImage();

                case ImageLibraryFormat.SKBitmap:
                    return ((SKBitmap)result).ToBitmapImage();

                case ImageLibraryFormat.MagickImage:
                    return ((IMagickImage)result).ToBitmapImage();

                case ImageLibraryFormat.ImageSharpImage:
                    return ((Image)result).ToBitmapImage();

                case ImageLibraryFormat.SystemDrawingBitmap:
                    return ((SysBitmap)result).ToBitmapImage();

                case ImageLibraryFormat.ByteArray:
                    return await ((byte[])result).ToBitmapImageAsync();

                case ImageLibraryFormat.EMGUCVImage:
                    return EmguResultToBitmapImage(result);

                case ImageLibraryFormat.ImageFlowBuildNode:
                    var skImage = await ((BuildNode)result).ToSKImageAsync();
                    return skImage.ToBitmapImage();

                case ImageLibraryFormat.WpfBitmapSource:
                    // WPF's System.Windows.Media.Imaging.BitmapSource can't be named in this
                    // WinUI project without enabling UseWPF, which breaks the WinUI XAML
                    // compiler when combined with UseWinUI in the same project. Benchmarking
                    // these converters still works (Benchmarks never names the WPF type);
                    // only the image preview is unavailable here.
                    throw new NotSupportedException("WPF BitmapSource results cannot be previewed in this WinUI app; use 'Run selected benchmarks' instead.");

                default:
                    throw new NotSupportedException($"No display adapter for target format {targetFormat}.");
            }
        }

        private static BitmapSource EmguResultToBitmapImage(object emguImage)
        {
            var imageType = emguImage.GetType();
            var typeArguments = imageType.GetGenericArguments();
            var method = typeof(EMGUCVExtensions)
                .GetMethod(nameof(EMGUCVExtensions.ToBitmapImage), BindingFlags.Public | BindingFlags.Static)!
                .MakeGenericMethod(typeArguments);

            return (BitmapSource)method.Invoke(null, new[] { emguImage })!;
        }
    }
}
