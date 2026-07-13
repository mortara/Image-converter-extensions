using PMortara.Helpers.ImageConverterExtensions;
using System.Reflection;

namespace TestAndBenchmark
{
    public sealed record ConverterDescriptor(MethodInfo Method, ImageConverterAttribute Attribute, string DisplayName);
}
