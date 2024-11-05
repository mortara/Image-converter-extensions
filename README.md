# Image converter extensions
 
A collection of (currently very quick&dirty) extension-methods to convert images between different library formats. 

Currently is covers images of: 
- SkiaSharp (https://github.com/mono/SkiaSharp)
- EMGU.CV (https://github.com/emgucv/emgucv)
- Magick.NET (https://github.com/dlemstra/Magick.NET)


Supported conersions:

 - SkiaSharp.SKImage <-> ImageMagick.IMagickImage
 - SkiaSharp.SKImage <-> Emgu.CV.Image
 - SkiaSharp.SKImage -> System.Drawing.Bitmap (with option to specify pixelformat)

 - SkiaSharp.SKBitmap <-> ImageMagick.IMagickImage
 - SkiaSharp.SKBitmap <-> Emgu.CV.Image
 - SkiaSharp.SKBitmap -> System.Drawing.Bitmap (with option to specify pixelformat)

 - System.Drawing.Icon -> SkiaSharp.SKImage

More extensions are about to come.



# License

Do whatever you want with it!

# Contact

https://www.mortara.org
https://talk.mls20.de/@Patrick
