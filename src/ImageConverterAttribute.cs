using System;
using System.Globalization;

namespace PMortara.Helpers.ImageConverterExtensions
{
    /// <summary>
    /// Library/type "formats" handled by this project's converters.
    /// These describe the in-memory representation (which imaging library owns
    /// the pixel data), not a file-encoding format like PNG or JPEG.
    /// </summary>
    /// <remarks>
    /// Named ImageLibraryFormat (not ImageFormat) to avoid colliding with the
    /// unqualified <see cref="System.Drawing.Imaging.ImageFormat"/> already used
    /// throughout the System.Drawing-based converters in this namespace.
    /// </remarks>
    public enum ImageLibraryFormat
    {
        Unknown = 0,
        SKImage,
        SKBitmap,
        MagickImage,
        EMGUCVImage,
        ImageSharpImage,
        SystemDrawingBitmap,
        SystemDrawingIcon,
        ByteArray,
        ImageFlowBuildNode,
        WinUIBitmapImage,
        WinUIWriteableBitmap,
        WpfBitmapSource,
    }

    /// <summary>
    /// Mirrors this project's To*/As* naming convention (see README.md):
    /// Real = "To*", creates a new copy of the pixel data.
    /// InMemoryWrapper = "As*", wraps existing pixel memory without copying.
    /// </summary>
    public enum ConverterKind
    {
        Real,
        InMemoryWrapper,
    }

    /// <summary>
    /// Declares metadata for an image converter extension method so it can be
    /// discovered and invoked via reflection (see TestAndBenchmark project)
    /// without the consumer needing to know about it ahead of time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class ImageConverterAttribute : Attribute
    {
        public string Version { get; }

        /// <summary>ISO-8601 date string ("yyyy-MM-dd"). DateTime/DateOnly are not valid attribute argument types.</summary>
        public string Date { get; }

        public ImageLibraryFormat SourceFormat { get; }

        public ImageLibraryFormat TargetFormat { get; }

        public ConverterKind Kind { get; }

        /// <summary>
        /// Closed generic type arguments to use when invoking this converter via
        /// reflection, in the order of the method's own type parameters. Required
        /// only when the annotated method is a generic method definition.
        /// </summary>
        public Type[]? GenericArguments { get; set; }

        public ImageConverterAttribute(string version, string date, ImageLibraryFormat sourceFormat, ImageLibraryFormat targetFormat, ConverterKind kind)
        {
            Version = version;
            Date = date;
            SourceFormat = sourceFormat;
            TargetFormat = targetFormat;
            Kind = kind;
        }

        public DateOnly ParsedDate => DateOnly.ParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}
