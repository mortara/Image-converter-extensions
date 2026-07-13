using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.IO;

namespace TestAndBenchmark
{
    /// <summary>
    /// Optionally stamps "TEST!!" centered onto the shared test image before it
    /// is loaded into the various source-library representations, so every
    /// converter run sees the same modification (or none).
    /// </summary>
    public static class TestOverlay
    {
        private const string OverlayText = "TEST!!";
        private const double FontScale = 8.0;
        private const int Thickness = 6;

        public static byte[] ApplyIfRequested(string imagePath, bool applyOverlay)
        {
            if (!applyOverlay)
                return File.ReadAllBytes(imagePath);

            using var image = new Image<Bgra, byte>(imagePath);

            int baseline = 0;
            var textSize = CvInvoke.GetTextSize(OverlayText, FontFace.HersheyPlain, FontScale, Thickness, ref baseline);

            var origin = new Point(
                (image.Width - textSize.Width) / 2,
                (image.Height + textSize.Height) / 2);

            image.Draw(OverlayText, origin, FontFace.HersheyPlain, FontScale, new Bgra(255, 0, 0, 255), Thickness);

            return image.ToJpegData();
        }
    }
}
