using System.Drawing.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SystemDrawingExtensions
    {
        public static Bitmap ConvertPixelFormat(this Bitmap source, PixelFormat targetFormat)
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
