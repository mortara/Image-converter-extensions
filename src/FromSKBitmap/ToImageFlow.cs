using Imageflow.Bindings;
using Imageflow.Fluent;
using SkiaSharp;
using System.Drawing;

namespace PMortara.Helpers.ImageConverterExtensions.FromSKBitmap
{
    public static partial class SKBitmapExtensions
    {
     
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.SKBitmap, ImageLibraryFormat.ImageFlowBuildNode, ConverterKind.Real)]
        public static BuildNode ToImageFlowBuildNode(this SKBitmap skbmp)
        {

            using (var pixmap = skbmp.PeekPixels())
            {
                var filters = SKPngEncoderFilterFlags.NoFilters;
                int compress = 0;
                var options = new SKPngEncoderOptions(filters, compress);
                using (var data = pixmap.Encode(options))
                {
                    var job = new ImageJob();
                    return job.Decode(data.ToArray());
                }
            }

        }
    }
}
