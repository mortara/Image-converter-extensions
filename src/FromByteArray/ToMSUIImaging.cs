using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ByteArrayExtensions
    {
        public static async Task<BitmapImage> GetBitmapImageAsync(this byte[] data)
        {
            var bitmapImage = new BitmapImage();
            using (var imageStream = new MemoryStream())
            {
                imageStream.Write(data, 0, data.Length);
                imageStream.Seek(0, SeekOrigin.Begin);
                await bitmapImage.SetSourceAsync(imageStream.AsRandomAccessStream());
            }

            return bitmapImage;
           
        }

        public static BitmapImage GetBitmapImage(this byte[] data)
        {

            var bitmapImage = new BitmapImage();
            using (var imageStream = new MemoryStream())
            {
                imageStream.Write(data, 0, data.Length);
                imageStream.Seek(0, SeekOrigin.Begin);
                bitmapImage.SetSource(imageStream.AsRandomAccessStream());
            }

            return bitmapImage;
        }
    }
}
