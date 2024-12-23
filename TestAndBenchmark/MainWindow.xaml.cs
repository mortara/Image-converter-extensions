using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using PMortara.Helpers.ImageConverterExtensions;
using PMortara.Helpers.ImageConverterExtensions.FromMagickNET;
using PMortara.Helpers.ImageConverterExtensions.FromSKBitmap;
using SixLabors.ImageSharp;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Windows;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Image = SixLabors.ImageSharp.Image;

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

        public async void TestConversions()
        {
            var path = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "Assets", "DSC_6947.JPG");

            var skimg = SKImage.FromEncodedData(path);
            AddConversionResult("SKImage -> ToWriteableBitmap", skimg.ToWriteableBitmap());

            var _EMGUCVIMage = new Image<Bgra, byte>(path);
            AddConversionResult("EMGUCV Image -> ToBitmapImage", _EMGUCVIMage.ToBitmapImage());

            var _MagickImage = new MagickImage(path);
            AddConversionResult("MagickImage -> ToBitmapImage", _MagickImage.ToBitmapImage());

            AddConversionResult("MagickImage -> ToWriteableBitmap", _MagickImage.ToWriteableBitmap());

            var emgucvimg = skimg.ToEMGUImage<Bgra, byte>();
            AddConversionResult("SKImage -> EMGUCV Image -> ToBitmapImage", emgucvimg.ToBitmapImage());

            var imagemagickimage = skimg.ToMagickImage();
            AddConversionResult("SKImage -> MagickImage -> ToBitmapImage", imagemagickimage.ToBitmapImage());

            var sysbitmap = skimg.ToBitmap();
            AddConversionResult("SKImage -> ToBitmap -> ToBitmapImage", sysbitmap.ToBitmapImage());

            var skbitmap = SKBitmap.FromImage(skimg);
            var emgucv2 = skbitmap.AsEMGUCVImage();
            emgucv2.Draw("T E S T ! !", new System.Drawing.Point(1000, 1000), Emgu.CV.CvEnum.FontFace.HersheyPlain, 50, new Bgra(255,0,0,255), 25 );
            AddConversionResult("SKImage -> SKBitmap.AsEMGUCV -> ToBitmapImage", emgucv2.ToBitmapImage());

            var skbitmap2 = SKBitmap.FromImage(skimg);
            var sysbmp = skbitmap2.AsBitmap();
            AddConversionResult("SKBitmap -> AsBitmap -> ToBitmapImage", sysbmp.ToBitmapImage());

            var magicimg2 = skbitmap2.ToMagickImage();
            AddConversionResult("SKBitmap -> ToMagickImage -> ToBitmapImage", magicimg2.ToBitmapImage());


            var imagesharpimg = Image.Load(path);
            AddConversionResult("ImageSharp.Image -> ToSKImage -> ToBitmapImage", imagesharpimg.ToSKImage().ToBitmapImage());

            var imageflowimg = skbitmap.ToImageFlowBuildNode();
            var ifsk = await imageflowimg.FlipVertical().ToSKImageAsync();
            AddConversionResult("SKBitmap -> ToImageFlowBuildNode -> FlipVertical -> ToSKImage -> ToBitmapImage", ifsk.ToBitmapImage());

            var skbitmap3 = SKBitmap.FromImage(skimg);
            AddConversionResult("SKBitmap -> ToBitmapImage", ifsk.ToBitmapImage());

            var isimage = imagemagickimage.ToImageSharpImage();
            AddConversionResult("SKBitmap -> ToImageSharpImage -> ToBitmapImage", isimage.ToBitmapImage());
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
            Benchmarks.Results = String.Empty;
            RunBenchmarks();
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            LoadImage();
            TestConversions();

        }
    }
}
