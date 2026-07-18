# Image Converter Extensions

A collection of extension methods to convert images between different .NET image library formats.

[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-blue.svg)](http://unlicense.org/)
[![.NET](https://img.shields.io/badge/.NET-C%23-blue)](https://dotnet.microsoft.com/)

## Overview

This library provides seamless conversion between popular .NET image processing libraries through convenient extension methods. Whether you're working with SkiaSharp, ImageMagick, EMGU.CV, or other image libraries, these extensions help you convert between formats with minimal friction.

## Supported Libraries

- **[SkiaSharp](https://github.com/mono/SkiaSharp)** - Cross-platform 2D graphics API
- **[EMGU.CV](https://github.com/emgucv/emgucv)** - .NET wrapper for OpenCV
- **[Magick.NET](https://github.com/dlemstra/Magick.NET)** - ImageMagick for .NET
- **[ImageSharp](https://sixlabors.com/products/imagesharp/)** - Modern image processing library
- **[ImageFlow dotNet](https://github.com/imazen/imageflow-dotnet)** - High-performance image manipulation
- **Microsoft.UI.Xaml.Media.Imaging.BitmapImage** - WinUI imaging
- **System.Windows.Media.Imaging** - WPF imaging
- **System.Drawing** - Classic .NET imaging

## Supported Conversions

### SkiaSharp.SKImage
- ↔️ ImageMagick.IMagickImage
- ↔️ Emgu.CV.Image
- → System.Drawing.Bitmap (with optional pixel format specification)

### SkiaSharp.SKBitmap
- ↔️ ImageMagick.IMagickImage
- ↔️ Emgu.CV.Image
- → System.Drawing.Bitmap (with optional pixel format specification)

### Other Conversions
- System.Drawing.Icon → SkiaSharp.SKImage

*More conversions are continuously being added.*

## Usage

### Installation

This library is not published as a NuGet package. The recommended approach is to copy the specific extension methods you need into your project.

### Naming Convention

- **`ToXXXX()`** methods (e.g., `ToSKImage()`) - Create a **new copy** of the image
- **`AsXXXX()`** methods - Do **not** create a copy, working with the existing data

### Example

```csharp
using SkiaSharp;
using ImageMagick;

// Convert SKImage to MagickImage (creates new copy)
SKImage skImage = ...;
IMagickImage magickImage = skImage.ToMagickImage();

// Convert back
SKImage converted = magickImage.ToSKImage();
```

## Development Goals

The library currently uses `System.Drawing.Image` as an intermediate format for some conversions. The ongoing goal is to eliminate these unnecessary intermediate steps and implement direct conversions wherever possible for better performance.

## Contributing

Contributions are highly welcomed and appreciated! Whether it's:
- Adding new conversion methods
- Optimizing existing conversions
- Improving documentation
- Reporting bugs

Feel free to open issues or submit pull requests.

## License

This project is licensed under [The Unlicense](LICENSE) - do whatever you want with it!

## Contact

- **Website:** [https://www.mortara.org](https://www.mortara.org)
- **Mastodon:** [https://talk.mls20.de/@Patrick](https://talk.mls20.de/@Patrick)

---

**Note:** This is currently a work in progress. Some conversions may be "quick & dirty" implementations and are subject to improvement.
