using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using PMortara.Helpers.ImageConverterExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestAndBenchmark
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; } = new ViewModel();

        private Benchmarks _benchmark = new Benchmarks();

        public Benchmarks Benchmarks { get { return _benchmark; } }

        public MainWindow()
        {
            this.InitializeComponent();

            foreach (var descriptor in ConverterCatalog.Discover())
                ViewModel.AvailableConverters.Add(new ConverterSelectionViewModel(descriptor));
        }

        private static String TestImagePath =>
            Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "Assets", "DSC_6947.JPG");

        private IReadOnlyDictionary<ImageLibraryFormat, object> BuildSources()
        {
            var bytes = TestOverlay.ApplyIfRequested(TestImagePath, ViewModel.OverlayTestText);
            return SourceInstanceFactory.CreateAll(bytes);
        }

        private static void DisposeSources(IReadOnlyDictionary<ImageLibraryFormat, object> sources)
        {
            foreach (var value in sources.Values)
                if (value is IDisposable disposable)
                    disposable.Dispose();
        }

        public async void RunSelectedTests()
        {
            ViewModel.Results.Clear();

            var sources = BuildSources();
            try
            {
                if (sources.TryGetValue(ImageLibraryFormat.ByteArray, out var originalBytes))
                    AddConversionResult("Original image", await ((byte[])originalBytes).ToBitmapImageAsync());

                foreach (var selection in ViewModel.AvailableConverters.Where(c => c.IsSelected))
                {
                    var descriptor = selection.Descriptor;
                    try
                    {
                        if (!sources.TryGetValue(descriptor.Attribute.SourceFormat, out var source))
                        {
                            AddConversionResult($"{descriptor.DisplayName}: skipped (no source for {descriptor.Attribute.SourceFormat})", null);
                            continue;
                        }

                        var result = await ConverterInvoker.InvokeAsync(descriptor, source);
                        var display = await ResultDisplayAdapter.ToDisplayImageAsync(result, descriptor.Attribute.TargetFormat);
                        AddConversionResult(descriptor.DisplayName, display);
                    }
                    catch (Exception ex)
                    {
                        AddConversionResult($"{descriptor.DisplayName}: FAILED - {ex.GetBaseException().Message}", null);
                    }
                }
            }
            finally
            {
                DisposeSources(sources);
            }
        }

        public async void RunSelectedBenchmarks()
        {
            Benchmarks.Results = String.Empty;

            var sources = BuildSources();
            try
            {
                var selected = ViewModel.AvailableConverters.Where(c => c.IsSelected).Select(c => c.Descriptor);
                await Benchmarks.RunSelectedAsync(selected, sources);
            }
            finally
            {
                DisposeSources(sources);
            }
        }

        private void AddConversionResult(String name, BitmapSource bmp)
        {
            this.DispatcherQueue?.TryEnqueue(() =>
            {
                var result = new ResultViewModel();
                result.Text = name;
                result.Image = bmp;
                ViewModel.Results.Add(result);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RunSelectedBenchmarks();
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            RunSelectedTests();
        }
    }
}
