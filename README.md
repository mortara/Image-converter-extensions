# Image converter extensions
 
A collection of (currently very quick&dirty) extension-methods to convert images between different library formats. 

Currently is covers images of: 
- SkiaSharp (https://github.com/mono/SkiaSharp)
- EMGU.CV (https://github.com/emgucv/emgucv)
- Magick.NET (https://github.com/dlemstra/Magick.NET)
- Microsoft.UI.Xaml.Media.Imaging.BitmapImage
- System.Drawing

Supported conversions:

 - SkiaSharp.SKImage <-> ImageMagick.IMagickImage
 - SkiaSharp.SKImage <-> Emgu.CV.Image
 - SkiaSharp.SKImage -> System.Drawing.Bitmap (with option to specify pixelformat)

 - SkiaSharp.SKBitmap <-> ImageMagick.IMagickImage
 - SkiaSharp.SKBitmap <-> Emgu.CV.Image
 - SkiaSharp.SKBitmap -> System.Drawing.Bitmap (with option to specify pixelformat)

 - System.Drawing.Icon -> SkiaSharp.SKImage

More extensions are about to come.

# Usage

I have no intentions to publish this as a nuget package or something like that. The best way to use it,
is probably to just copy and paste the parts you really need into your own project.

# License

Do whatever you want with it!

# Contact

https://www.mortara.org

https://talk.mls20.de/@Patrick
