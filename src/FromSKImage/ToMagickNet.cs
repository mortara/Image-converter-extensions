using ImageMagick;
using SkiaSharp;


namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts SKBitmap to IMagickImage
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Improve handling of different pixelformats
        /// </ToDo>
        public static IMagickImage ToMagickImage(this SKImage skimg)
        {
            if(skimg.IsLazyGenerated)
            {
                using (var bmp = SKBitmap.FromImage(skimg))
                    return bmp.ToMagickImage();
            }

            using (var pixeldata = skimg.PeekPixels())
            {
                var pixels = pixeldata.GetPixelSpan();

                var settings = new MagickReadSettings();
                settings.Width = (uint)skimg.Width;
                settings.Height = (uint)skimg.Height;

                switch (skimg.ColorType)
                {
                    case SKColorType.Rgba8888:
                        settings.Depth = 8;
                        settings.Format = MagickFormat.Rgba;
                        break;
                    case SKColorType.Bgra8888:
                        settings.Depth = 8;
                        settings.Format = MagickFormat.Bgra;
                        break;
                }

                return new MagickImage(pixels, settings);
            }
        }

    }
}
