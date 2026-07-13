using System.Drawing.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SystemDrawingExtensions
    {
        [ImageConverter("1.0", "2026-07-13", ImageLibraryFormat.SystemDrawingBitmap, ImageLibraryFormat.SystemDrawingBitmap, ConverterKind.Real)]
        public static Bitmap ConvertPixelFormat(this Bitmap source, PixelFormat targetFormat = PixelFormat.Format32bppArgb)
        {
            var bmpnew = new Bitmap(source.Width, source.Height, targetFormat);
            using (var g = Graphics.FromImage(bmpnew))
            {
                g.DrawImage(source, new PointF(0, 0));
            }

            return bmpnew;
        }
    }
}
