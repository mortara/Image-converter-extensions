# Image converter extensions
 
A collection of (currently very quick & dirty) extension-methods to convert images between different library formats. 

Currently it covers images of: 
- SkiaSharp (https://github.com/mono/SkiaSharp)
- EMGU.CV (https://github.com/emgucv/emgucv)
- Magick.NET (https://github.com/dlemstra/Magick.NET)
- ImageSharp (https://sixlabors.com/products/imagesharp/)
- ImageFlow dotNet (https://github.com/imazen/imageflow-dotnet)
- Microsoft.UI.Xaml.Media.Imaging.BitmapImage
- System.Windows.Media.Imaging
- System.Drawing

Most conversion use a intermediate step over a System.Drawing.Image. My goal ist to get rid
of all these unnecessary steps and make direct conversions if possible.

Some of the supported conversions:

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

All conversions that start with ToXXXX() (e.g. ToSKImage()) are meant to create a new copy of the image.
Later i want to also add AsXXXX() function that do not create a new copy.

# Contribution

Any kinf of contribution is highly welcomed and appriciated!

# License

Do whatever you want with it!

# Contact

https://www.mortara.org

https://talk.mls20.de/@Patrick
