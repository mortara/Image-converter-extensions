using Imageflow.Bindings;
using Imageflow.Fluent;
using ImageMagick;
using SkiaSharp;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{


    public static partial class ImageFlowExtensions
    {
     
        /// <summary>
        /// Converts IMageFlow BuildNode to SKImage
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        public static async Task<SKImage> ToSKImageAsync(this BuildNode mimg)
        {

                var r = await mimg.EncodeToBytes(new Imageflow.Fluent.PngQuantEncoder()).Finish().InProcessAsync();
                var bytes = r.First.TryGetBytes().Value;
         
                return SKImage.FromEncodedData(bytes.ToArray());
            
        }

        /// <summary>
        /// Converts IMageFlow to SKBitmap
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of Bmp3 re-encoding
        /// </ToDo>
        public static async Task<SKBitmap> ToSKBitmapAsync(this BuildNode mimg)
        {
            var r = await mimg.EncodeToBytes(new Imageflow.Fluent.PngQuantEncoder()).Finish().InProcessAsync();
            var bytes = r.First.TryGetBytes().Value;

            return SKBitmap.Decode(bytes.ToArray());

        }
    }
}
