using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using PMortara.Helpers.ImageConverterExtensions;
using PMortara.Helpers.ImageConverterExtensions.FromSKBitmap;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace TestAndBenchmark
{
    /// <summary>
    /// Loads the shared test image bytes into one instance per underlying imaging
    /// library. This mapping is tied to the fixed set of libraries this project
    /// supports, not to individual converters -- a new converter between two
    /// already-supported libraries needs no change here.
    /// </summary>
    public static class SourceInstanceFactory
    {
        public static IReadOnlyDictionary<ImageLibraryFormat, object> CreateAll(byte[] sourceBytes)
        {
            var map = new Dictionary<ImageLibraryFormat, object>();

            var skBitmap = SKBitmap.Decode(sourceBytes);
            map[ImageLibraryFormat.SKBitmap] = skBitmap;
            map[ImageLibraryFormat.SKImage] = SKImage.FromBitmap(skBitmap);

            map[ImageLibraryFormat.MagickImage] = new MagickImage(sourceBytes);

            var sysBitmap = new Bitmap(new MemoryStream(sourceBytes));
            map[ImageLibraryFormat.SystemDrawingBitmap] = sysBitmap;
            map[ImageLibraryFormat.SystemDrawingIcon] = Icon.FromHandle(sysBitmap.GetHicon());

            // Canonical EMGU representation: Image<Bgra, byte>. Matches the
            // GenericArguments declared on every EMGU-sourced [ImageConverter].
            map[ImageLibraryFormat.EMGUCVImage] = new Image<Bgra, byte>(sysBitmap);

            // Canonical ImageSharp representation. Image<Rgb24> derives from
            // Image, so this single instance also satisfies converters whose
            // "this" parameter type is the non-generic base Image.
            map[ImageLibraryFormat.ImageSharpImage] = Image.Load<Rgb24>(sourceBytes);

            map[ImageLibraryFormat.ByteArray] = sourceBytes;

            map[ImageLibraryFormat.ImageFlowBuildNode] = skBitmap.ToImageFlowBuildNode();

            return map;
        }
    }
}
