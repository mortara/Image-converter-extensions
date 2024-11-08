using BenchmarkDotNet.Running;
using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using PMortara.Helpers.ImageConverterExtensions;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestAndBenchmark
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
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

            imagecontrol.Source = new BitmapImage(new Uri(path, UriKind.Absolute));

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
            var sp = new StackPanel();

            var tb = new TextBlock();
            tb.Text = name;
            tb.MaxWidth = 200;
            tb.TextWrapping = TextWrapping.Wrap;    
            tb.VerticalAlignment = VerticalAlignment.Bottom;
            sp.Children.Add(tb);

            var img = new Image();
            img.Margin = new Thickness(10);
            img.Width = 200;
            img.Height = 200;
            img.Stretch = Stretch.Uniform;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.Source = bmp;   
            sp.Children.Add(img);

            resultgrid.Children.Add(sp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Benchmarks.Results = String.Empty;
            RunBenchmarks();
        }
    }
}
