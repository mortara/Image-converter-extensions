using CommunityToolkit.Mvvm.ComponentModel;
using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using PMortara.Helpers.ImageConverterExtensions;
using SixLabors.ImageSharp;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Windows;
using System;
using System.Diagnostics;
using System.IO;


namespace TestAndBenchmark
{


    public partial class Benchmarks : ObservableObject
    {
        private String ImagePath { get;set ; } = String.Empty;

        private Image<Bgra, byte> _EMGUCVIMage = null;
        private SKImage _SKImage = null;
        private SKBitmap _SKBitmap = null;
        private IMagickImage _MagickImage = null;
        private System.Drawing.Bitmap _SysBitmap = null;
        private Image _ImageSharpImage = null;

        [ObservableProperty]
        private String results = String.Empty;

        public void Setup()
        {
            Debug.Print("Setup");

            ImagePath = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "Assets", "DSC_6947.JPG");
            Debug.Print(ImagePath);
            _EMGUCVIMage = new Image<Bgra, byte>(ImagePath);
            _SKImage = SKImage.FromEncodedData(ImagePath);
            _SKBitmap = SKBitmap.FromImage(_SKImage);
            _MagickImage = new MagickImage(ImagePath);
            _ImageSharpImage = Image.Load(ImagePath);   
            _SysBitmap = _EMGUCVIMage.ToBitmap();
        }

        public void RunBenchmarks()
        {
            RunTests("EMGUCV to BitmapImage", () => { return _EMGUCVIMage.ToBitmapImage(); });
            
            RunTests("EMGUCV to WriteableBitmap", () => { return _EMGUCVIMage.ToWriteableBitmap(); });

            RunTests("EMGUCV to SKImage", () => { return _EMGUCVIMage.ToSKImage(); });

            RunTests("EMGUCV to SKBitmap", () => { return _EMGUCVIMage.ToSKBitmap(); });

            //RunTests("EMGUCV to MagickImage", () => { return _EMGUCVIMage.ToSKImage(); });

            //RunTests("SKBitmap to BitmapImage", () => { return _SKBitmap.ToBitmapImage(); });

            //RunTests("SKImage to BitmapImage", () => { return _SKImage.ToBitmapImage(); });

            RunTests("SKImage to WriteableBitmap", () => { return _SKImage.ToWriteableBitmap(); });

            RunTests("SKImage to MagickImage", () => { return _SKImage.ToMagickImage(); });

            /*var bmp = SKBitmap.FromImage(_SKImage);
            var img = SKImage.FromBitmap(bmp);
            RunTests("SKImage to MagickImage ", () => { return img.ToMagickImage(); });*/

            RunTests("SKImage to System.Drawing.Bitmap", () => { return _SKImage.ToBitmap(System.Drawing.Imaging.PixelFormat.Format32bppArgb); });

            RunTests("Bitmap to BitmapImage", () => { return _SysBitmap.ToBitmapImage(); });

            RunTests("SKBitmap to MagickImage", () => { return _SKBitmap.ToMagickImage(); });

            RunTests("ImageSharp.Image to SKImage", () => { return _ImageSharpImage.ToSKImage(); });
        }

        public void RunTests(String name, Func<object> action, int cnt = 10)
        {
            Debug.Print("Start test: " + name);
            GC.Collect();
            var mem = GC.GetAllocatedBytesForCurrentThread();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10; i++)
            {
                var bmp = action();
                
                if(bmp is IDisposable disposable)
                    disposable.Dispose();
            }
            sw.Stop();
            GC.Collect();
            var usage = (float)(GC.GetAllocatedBytesForCurrentThread() - mem) / 1024f;

            AddResult($"{cnt} x {name}: {sw.ElapsedMilliseconds} ms. Memory usage: {usage} KB");
        }

        private void AddResult(String text)
        {
            Debug.Print($"{text}");
            Results = $"{Results}\r\n{text}";
        }
    }
}
