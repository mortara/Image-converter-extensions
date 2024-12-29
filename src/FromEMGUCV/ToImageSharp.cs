using Emgu.CV;
using Emgu.CV.Structure;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using System;
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

        /// <summary>
        /// Wraps a EMGU.CV image into a ImageSharp Image without copying the pixel-data
        /// </summary>
        /// <typeparam name="TColor"></typeparam>
        /// <typeparam name="TDepth"></typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Image AsImageSharpImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            Type typeFromHandle = typeof(TColor);
            Type depthFromHandle = typeof(TDepth);
            unsafe
            {
                var l = image.NumberOfChannels * image.Width * image.Height;
                var memory = image.Mat.DataPointer.ToPointer();

                if (typeFromHandle == typeof(Gray))
                {
                    return Image.WrapMemory<L8>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Bgra))
                {
                    return Image.WrapMemory<Bgra32>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Rgba))
                {
                    return Image.WrapMemory<Rgba32>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Bgr))
                {
                    return Image.WrapMemory<Bgr24>(memory, l, image.Width, image.Height);
                }
                else if (typeFromHandle == typeof(Rgb))
                {
                    return Image.WrapMemory<Rgb24>(memory, l, image.Width, image.Height);
                }
            }

            throw new Exception("ColorType not supported!");
        }
    }
}
