# Image converter extensions
 
A collection of quick&dirty extension-methods to convert images between different library formats. As i am using SkiaSharp primary, these extensions are mostly build around SkiaSharps SKImage class.

Currently supported:

 - SkiaSharp.SKImage <-> ImageMagick.IMagickImage
 - SkiaSharp.SKImage -> Emgu.CV.Image

 - SkiaSharp.SKBitmap <-> ImageMagick.IMagickImage
 - SkiaSharp.SKBitmap -> Emgu.CV.Image

 - SkiaSharp.SKImage -> Microsoft.UI.Xaml.Media.Imaging.BitmapSource
 - SkiaSharp.SKImage -> Microsoft.UI.Xaml.Media.Imaging.BitmapImage

 - SkiaSharp.SKBitmap -> Microsoft.UI.Xaml.Media.Imaging.BitmapSource
 - SkiaSharp.SKBitmap -> Microsoft.UI.Xaml.Media.Imaging.BitmapImage

 - System.Drawing.Icon -> SkiaSharp.SKImage
 - System.Drawing.Bitmap -> SkiaSharp.SKImage

 - System.Drawing.Icon -> SkiaSharp.SKBitmap
 - System.Drawing.Bitmap -> SkiaSharp.SKBitmap

More extensions are about to come.



# License

Do whatever you want with it!

# Contact

https://www.mortara.org
https://talk.mls20.de/@Patrick
