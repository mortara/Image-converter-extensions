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

        public static byte[] ApplyIfRequested(string imagePath, bool applyOverlay)
        {
            if (!applyOverlay)
                return File.ReadAllBytes(imagePath);

            using var image = new Image<Bgra, byte>(imagePath);

            // Scale text to the actual image resolution -- a fixed small
            // font size is invisible on a multi-thousand-pixel photo. This
            // yields ~50/25 on the 6048x4024 default test image, matching
            // values already proven visible in earlier ad-hoc Draw() calls.
            double fontScale = System.Math.Max(1.0, image.Width / 120.0);
            int thickness = System.Math.Max(1, (int)(fontScale / 2));

            int baseline = 0;
            var textSize = CvInvoke.GetTextSize(OverlayText, FontFace.HersheyPlain, fontScale, thickness, ref baseline);

            var origin = new Point(
                (image.Width - textSize.Width) / 2,
                (image.Height + textSize.Height) / 2);

            image.Draw(OverlayText, origin, FontFace.HersheyPlain, fontScale, new Bgra(0, 0, 255, 255), thickness);

            return image.ToJpegData();
        }
    }
}
