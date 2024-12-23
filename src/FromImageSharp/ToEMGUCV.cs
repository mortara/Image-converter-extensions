using Emgu.CV;
using Emgu.CV.Structure;
using SixLabors.ImageSharp;
using System.IO;
using System.Windows.Media.Media3D;
using Image = SixLabors.ImageSharp.Image;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ImageSharpExtensions
    {
        /// <summary>
        /// Converts IMageSharp Image to Emgu.CV Image
        /// </summary>
        /// <param name="skimg"></param>
        /// <returns></returns>
        /// <ToDo>
        /// Get rid of that ToBitmap() step.
        /// </ToDo>
        public static Image<TColor, TDepth> ToEMGUImage<TColor, TDepth>(this Image ims) where TColor : struct, IColor where TDepth : new()
        {
           
            try
            {
                using (var ms = new MemoryStream())
                {
                    ims.SaveAsBmp(ms);
                    ms.Position = 0;

                    var imageFromBytes = new Image<TColor, TDepth>(ims.Width, ims.Height);

                    CvInvoke.Imdecode(ms.ToArray(), Emgu.CV.CvEnum.ImreadModes.AnyColor, imageFromBytes.Mat);

                    return imageFromBytes;
                }

            }
            finally
            {
               
            }
        }

    }
}
