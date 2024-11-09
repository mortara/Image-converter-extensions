using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class EMGUCVExtensions
    {
        public static System.Windows.Media.Imaging.BitmapSource ToWPFBitmapImage<TColor, TDepth>(this Image<TColor, TDepth> image) where TColor : struct, IColor where TDepth : new()
        {
            var bmp = image.ToBitmap();
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmp.GetHbitmap(),
                    nint.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
        }
    }
}
