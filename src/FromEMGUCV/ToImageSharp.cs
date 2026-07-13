using Emgu.CV;
using Emgu.CV.Structure;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using System;
using ImageSharpImage = SixLabors.ImageSharp.Image;

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
        [ImageConverter("1.0", "2024-12-23", ImageLibraryFormat.EMGUCVImage, ImageLibraryFormat.ImageSharpImage, ConverterKind.Real,
            GenericArguments = new[] { typeof(Bgra), typeof(byte) })]
        public static ImageSharpImage ToImageSharpImage_v1<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            var jpegbytes = image.ToJpegData();

            return ImageSharpImage.Load(jpegbytes);
        }

        /// <summary>
        /// Converts a EMGU.CV Image into a ImageSharp.Image via a direct pixel-path
        /// (no JPEG re-encoding), falling back to a Bgra/byte conversion for
        /// unsupported color/depth combinations.
        /// </summary>
        [ImageConverter("2.0", "2026-07-13", ImageLibraryFormat.EMGUCVImage, ImageLibraryFormat.ImageSharpImage, ConverterKind.Real,
            GenericArguments = new[] { typeof(Bgra), typeof(byte) })]
        public static ImageSharpImage ToImageSharpImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            var typeFromHandle = typeof(TColor);
            var depthFromHandle = typeof(TDepth);

            if (depthFromHandle != typeof(byte))
            {
                using var converted = image.Convert<Bgra, byte>();
                return converted.ToImageSharpImage();
            }

            var bytes = image.Bytes;

            if (typeFromHandle == typeof(Gray))
                return ImageSharpImage.LoadPixelData<L8>(bytes, image.Width, image.Height);

            if (typeFromHandle == typeof(Bgra))
                return ImageSharpImage.LoadPixelData<Bgra32>(bytes, image.Width, image.Height);

            if (typeFromHandle == typeof(Rgba))
                return ImageSharpImage.LoadPixelData<Rgba32>(bytes, image.Width, image.Height);

            if (typeFromHandle == typeof(Bgr))
                return ImageSharpImage.LoadPixelData<Bgr24>(bytes, image.Width, image.Height);

            if (typeFromHandle == typeof(Rgb))
                return ImageSharpImage.LoadPixelData<Rgb24>(bytes, image.Width, image.Height);

            using (var converted = image.Convert<Bgra, byte>())
                return converted.ToImageSharpImage();
        }

        /// <summary>
        /// Wraps a EMGU.CV image into a ImageSharp Image without copying the pixel-data
        /// </summary>
        /// <typeparam name="TColor"></typeparam>
        /// <typeparam name="TDepth"></typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [ImageConverter("1.0", "2024-12-29", ImageLibraryFormat.EMGUCVImage, ImageLibraryFormat.ImageSharpImage, ConverterKind.InMemoryWrapper,
            GenericArguments = new[] { typeof(Bgra), typeof(byte) })]
        public static ImageSharpImage AsImageSharpImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            Type typeFromHandle = typeof(TColor);
            Type depthFromHandle = typeof(TDepth);
            unsafe
            {
                var l = image.NumberOfChannels * image.Width * image.Height;
                var memory = image.Mat.DataPointer.ToPointer();

                if (typeFromHandle == typeof(Gray))
                {
                    return ImageSharpImage.WrapMemory<L8>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Bgra))
                {
                    return ImageSharpImage.WrapMemory<Bgra32>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Rgba))
                {
                    return ImageSharpImage.WrapMemory<Rgba32>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Bgr))
                {
                    return ImageSharpImage.WrapMemory<Bgr24>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Rgb))
                {
                    return ImageSharpImage.WrapMemory<Rgb24>(memory, l, image.Width, image.Height);
                }
            }

            throw new Exception("ColorType not supported!");
        }
    }
}
