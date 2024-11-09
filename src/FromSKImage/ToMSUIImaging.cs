﻿using SkiaSharp;
using System.IO;
using Microsoft.UI.Xaml.Media.Imaging;

namespace PMortara.Helpers.ImageConverterExtensions
{
    public static partial class SKImageExtensions
    {
        /// <summary>
        /// Converts a SKImage to a BitmapImage. Kind of useless, since SKImage has it's own ToWriteableBitmap()
        /// </summary>
        /// <param name="skiaImage"></param>
        /// <returns></returns>
        public static BitmapImage ToBitmapImage(this SKImage skiaImage)
        {
            var bitmapImage = new BitmapImage();

            var encoded = skiaImage.Encode();
            using (var ms = new MemoryStream())
            {
                encoded.SaveTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                bitmapImage.SetSource(ms.AsRandomAccessStream());
            }

            return bitmapImage;

        }
    }
}