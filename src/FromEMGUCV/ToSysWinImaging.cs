using Emgu.CV;
using System.Windows.Media.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class EMGUCVExtensions
    {
        public static BitmapSource ToWPFBitmapSource<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            var bmp = image.AsBitmap();
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmp.GetHbitmap(),
                    nint.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
        }
    }
}
