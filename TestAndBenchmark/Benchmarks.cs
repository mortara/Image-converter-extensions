﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using CommunityToolkit.Mvvm.ComponentModel;
using Emgu.CV;
using Emgu.CV.Structure;
using ImageMagick;
using PMortara.Helpers.ImageConverterExtensions;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;


namespace TestAndBenchmark
{
   

    public partial class Benchmarks : ObservableObject
    {
        private String ImagePath { get;set ; } = String.Empty;

        private Image<Bgra, byte> _EMGUCVIMage = null;
        private SKImage _SKImage = null;
        private SKBitmap _SKBitmap = null;
        private IMagickImage _MagickImage = null;

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
        }

        public void RunBenchmarks()
        {
            RunTests("EMGUCV to BitmapImage", () => { return _EMGUCVIMage.ToBitmapImage(); });

            RunTests("EMGUCV to SKImage", () => { return _EMGUCVIMage.ToSKImage(); });

            //RunTests("EMGUCV to MagickImage", () => { return _EMGUCVIMage.ToSKImage(); });

            RunTests("SKBitmap to BitmapImage", () => { return _SKBitmap.ToBitmapImage(); });

            RunTests("SKImage to BitmapImage", () => { return _SKImage.ToBitmapImage(); });

            RunTests("SKImage to MagickImage", () => { return _SKImage.ToMagickImage(); });

            RunTests("SKImage to System.Drawing.Bitmap", () => { return _SKImage.ToBitmap(System.Drawing.Imaging.PixelFormat.Format32bppArgb); });

            RunTests("SKImage to EMGUCV Image<Bgra, byte>", () => { return _SKImage.ToEMGUImage<Bgra, byte>(); });
        }

        public void RunTests(String name, Func<object> action, int cnt = 10)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10; i++)
            {
                var bmp = action();
            }
            sw.Stop();

            AddResult($"10 x {name}: {sw.ElapsedMilliseconds} ms");
        }

        private void AddResult(String text)
        {
            Results = $"{Results}\r\n{text}";
        }
    }
}