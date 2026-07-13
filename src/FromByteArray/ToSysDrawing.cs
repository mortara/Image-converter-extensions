using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMortara.Helpers.ImageConverterExtensions.FromByteArray
{
    public static partial class ByteArrayExtensions
    {
        [ImageConverter("1.0", "2024-11-18", ImageLibraryFormat.ByteArray, ImageLibraryFormat.SystemDrawingBitmap, ConverterKind.Real)]
        public static System.Drawing.Image ToDrawingImage(this byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                return returnImage;
            }
        }

    }
}
