using Emgu.CV;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts SKImage to Emgu.CV Image
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that ToBitmap() step.
        /// </ToDo>
        public static Image<TColor, TDepth> ToEMGUImage<TColor, TDepth>(this SKImage img) where TColor : struct, IColor where TDepth : new()
        {
            var bmp = img.ToBitmap(PixelFormat.Format32bppArgb);

            try
            {
                return bmp.ToImage<TColor, TDepth>();
            }
            finally
            {
                bmp.Dispose();
            }
        }
    }
}
