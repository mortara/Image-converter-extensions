using PMortara.Helpers.ImageConverterExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestAndBenchmark
{
    /// <summary>
    /// Discovers every converter extension method annotated with [ImageConverter]
    /// in the ImageConverterExtensions assembly via reflection. Adding a new
    /// converter method there (with the attribute) makes it show up here
    /// automatically -- no change needed in this project.
    /// </summary>
    public static class ConverterCatalog
    {
        public static IReadOnlyList<ConverterDescriptor> Discover()
        {
            var assembly = typeof(ImageConverterAttribute).Assembly;

            var descriptors = new List<ConverterDescriptor>();

            foreach (var type in assembly.GetTypes())
            {
                if (!(type.IsAbstract && type.IsSealed && type.IsClass))
                    continue;

                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    var attribute = method.GetCustomAttribute<ImageConverterAttribute>();
                    if (attribute is null)
                        continue;

                    var displayName = $"{type.Name}.{method.Name} ({attribute.SourceFormat} -> {attribute.TargetFormat}, {attribute.Kind}, v{attribute.Version}, {attribute.Date})";
                    descriptors.Add(new ConverterDescriptor(method, attribute, displayName));
                }
            }

            return descriptors
                .OrderBy(d => d.Attribute.SourceFormat)
                .ThenBy(d => d.DisplayName, StringComparer.Ordinal)
                .ToList();
        }
    }
}
