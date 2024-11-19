using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class ByteArrayExtensions
    {
        public static async Task<BitmapImage> ToBitmapImageAsync(this byte[] data)
        {
            var bitmapImage = new BitmapImage();
            using (var imageStream = new MemoryStream(data))
            {
                await bitmapImage.SetSourceAsync(imageStream.AsRandomAccessStream());
            }

            return bitmapImage;
           
        }

        public static BitmapImage ToBitmapImage(this byte[] data)
        {

            var bitmapImage = new BitmapImage();
            using (var imageStream = new MemoryStream(data))
            {
                bitmapImage.SetSource(imageStream.AsRandomAccessStream());
            }

            return bitmapImage;
        }

    }
}
