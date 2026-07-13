using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using System.IO;
using System.Runtime.InteropServices;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ImageSharpExtensions
    {
        /// <summary>
        /// Converts ImageSharp.Image to SKImage
        /// </summary>
        /// <param name="ims"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        [ImageConverter("1.0", "2024-11-18", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SKImage, ConverterKind.Real)]
        public static SKImage ToSKImage_v1(this Image ims)
        {
            using (var ms = new MemoryStream())
            {
                ims.SaveAsBmp(ms);
                ms.Position = 0;
                return SKImage.FromEncodedData(ms);
            }
        }

        [ImageConverter("2.0", "2026-07-13", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SKImage, ConverterKind.Real)]
        public static SKImage ToSKImage_v2(this Image imageSharpImage)
        {
            SKImageInfo info = SKImageInfo.Empty;
            byte[] pixelData = null;

            if (imageSharpImage is Image<Rgba32> im)
            {
                // Create SKImageInfo with the same dimensions and color type
                info = new SKImageInfo(imageSharpImage.Width, imageSharpImage.Height, SKColorType.Rgba8888, SKAlphaType.Premul);

                // Allocate pixel buffer
                pixelData = new byte[info.BytesSize];

                // Copy pixel data from ImageSharp image to pixel buffer
                im.CopyPixelDataTo(pixelData);
            } 

            if (info == SKImageInfo.Empty || pixelData == null)
                return imageSharpImage.ToSKImage_v1();

            GCHandle handle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
            try
            {
                // Create SKImage from pixel buffer
                var skImage = SKImage.FromPixels(info, handle.AddrOfPinnedObject(), info.RowBytes);
                return skImage;
            }
            finally
            {
                // Free the pinned handle
                handle.Free();
            }
        }

        [ImageConverter("3.0", "2026-07-13", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SKImage, ConverterKind.Real)]
        public static SKImage ToSKImage(this Image ims)
        {
            using var bitmap = ims.ToSKBitmap();
            return SKImage.FromBitmap(bitmap);
        }

        /// <summary>
        /// Converts ImageSharp.Image to SKBitmap
        /// </summary>
        /// <param name="ims"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        [ImageConverter("1.0", "2024-11-18", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SKBitmap, ConverterKind.Real)]
        public static SKBitmap ToSKBitmap_v1(this Image ims)
        {
            using (var ms = new MemoryStream())
            {
                ims.SaveAsBmp(ms);
                ms.Position = 0;
                return SKBitmap.Decode(ms);
            }
        }

        [ImageConverter("2.0", "2026-07-13", ImageLibraryFormat.ImageSharpImage, ImageLibraryFormat.SKBitmap, ConverterKind.Real)]
        public static SKBitmap ToSKBitmap(this Image ims)
        {
            using (var rgba = ims.CloneAs<Rgba32>())
            {
                var info = new SKImageInfo(rgba.Width, rgba.Height, SKColorType.Rgba8888, SKAlphaType.Unpremul);
                var bitmap = new SKBitmap(info);

                rgba.ProcessPixelRows(accessor =>
                {
                    var target = bitmap.GetPixelSpan();
                    var targetStride = bitmap.RowBytes;
                    var sourceStride = rgba.Width * 4;

                    for (int y = 0; y < rgba.Height; y++)
                    {
                        var sourceRow = MemoryMarshal.AsBytes(accessor.GetRowSpan(y));
                        sourceRow.CopyTo(target.Slice(y * targetStride, sourceStride));
                    }
                });

                return bitmap;
            }
        }
    }
}
