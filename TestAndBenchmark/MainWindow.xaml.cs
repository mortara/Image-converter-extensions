using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using PMortara.Helpers.ImageConverterExtensions;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Windows;
using System;
using System.IO;

namespace TestAndBenchmark
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; } = new ViewModel();

        private String _testImage = String.Empty;
        private Benchmarks _benchmark  = new Benchmarks();

        public Benchmarks Benchmarks { get { return _benchmark; } }

        public MainWindow()
        {
            this.InitializeComponent();

            LoadImage();
            TestConversions();
        }

        public void RunBenchmarks()
        {
            _benchmark.Setup();
            _benchmark.RunBenchmarks();
      
        }

        public void LoadImage()
        {
            var path = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "Assets", "DSC_6947.JPG");
            AddConversionResult("Original image", new BitmapImage(new Uri(path, UriKind.Absolute)));

        }

        public void TestConversions()
        {
            var path = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "Assets", "DSC_6947.JPG");

            var skimg = SKImage.FromEncodedData(path);
            AddConversionResult("SKImage -> ToWriteableBitmap", skimg.ToWriteableBitmap());

            var _EMGUCVIMage = new Image<Bgra, byte>(path);
            AddConversionResult("EMGUCV Image -> BitmapImage", _EMGUCVIMage.ToBitmapImage());

            var _MagickImage = new MagickImage(path);
            AddConversionResult("MagickIMage -> BitmapImage", _MagickImage.ToBitmapImage());

            var emgucvimg = skimg.ToEMGUImage<Bgra, byte>();
            AddConversionResult("SKImage -> EMGUCV Image -> BitmapImage", emgucvimg.ToBitmapImage());

            var imagemagickimage = skimg.ToMagickImage();
            AddConversionResult("SKImage -> MagickImage -> BitmapImage", imagemagickimage.ToBitmapImage());

            var sysbitmap = skimg.ToBitmap();
            AddConversionResult("SKImage -> Bitmap -> BitmapImage", sysbitmap.ToBitmapImage());

        }

        private void AddConversionResult(String name, BitmapSource bmp)
        {
            var result = new ResultViewModel();
            result.Text = name;
            result.Image = bmp;
            ViewModel.Results.Add(result);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Benchmarks.Results = String.Empty;
            RunBenchmarks();
        }
    }
}
