using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace WindowsFormsApp
{
    public partial class Form3 : Form
    {
        private VideoCapture _capture;
        private Mat _frame;
        private Mat _edges;
        private Timer _timer;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            _capture = new VideoCapture(0);
            if (!_capture.IsOpened())
            {
                MessageBox.Show("Kamera açılamadı!");
                return;
            }

            _frame = new Mat();
            _edges = new Mat();
            _timer = new Timer();
            _timer.Interval = 30; // 30 ms aralıkla görüntü güncelleme
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _capture.Read(_frame);
            if (_frame.Empty())
            {
                MessageBox.Show("Kameradan görüntü alınamadı!");
                _timer.Stop();
                return;
            }

            _edges = PreprocessImage(_frame);
            var (solidity, wearAmount) = DetectDeformation(_edges);
            var classification = ClassifyDeformation(solidity, wearAmount);

            Cv2.PutText(_frame, $"Status: {classification}", new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1, Scalar.Green, 2);

            pictureBox1.Image = BitmapConverter.ToBitmap(_frame);
            pictureBox2.Image = BitmapConverter.ToBitmap(_edges);
        }

        private Mat PreprocessImage(Mat image)
        {
            var gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
            var blurred = new Mat();
            Cv2.GaussianBlur(gray, blurred, new OpenCvSharp.Size(5, 5), 0);
            var edges = new Mat();
            Cv2.Canny(blurred, edges, 50, 150);
            return edges;
        }

        private (double?, double?) DetectDeformation(Mat edges)
        {
            Cv2.FindContours(edges, out OpenCvSharp.Point[][] contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            if (contours.Length > 0)
            {
                var largestContour = contours.OrderByDescending(c => Cv2.ContourArea(c)).First();
                var area = Cv2.ContourArea(largestContour);
                var perimeter = Cv2.ArcLength(largestContour, true);
                var hull = Cv2.ConvexHull(largestContour);
                var hullArea = Cv2.ContourArea(hull);
                double solidity = hullArea > 0 ? (double)area / hullArea : 0;

                var corners = Cv2.GoodFeaturesToTrack(edges, 2, 0.01, 10, null, 3, false, 0.04);
                double wearAmount = 0;
                if (corners.Length > 0)
                {
                    var cornerDistances = new List<double>();
                    foreach (var corner in corners)
                    {
                        var dist = Cv2.PointPolygonTest(largestContour, corner, true);
                        cornerDistances.Add(Math.Abs(dist));
                    }
                    wearAmount = cornerDistances.Average();
                }

                return (solidity, wearAmount);
            }
            else
            {
                return (null, null);
            }
        }

        private string ClassifyDeformation(double? solidity, double? wearAmount, double solidityThreshold = 0.9, double wearThreshold = 10)
        {
            if (solidity == null || wearAmount == null)
            {
                return "unavailable";
            }
            else if (solidity > solidityThreshold && wearAmount < wearThreshold)
            {
                return "available";
            }
            else
            {
                return "unavailable";
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
            _capture.Release();
            _frame.Dispose();
            _edges.Dispose();
            Cv2.DestroyAllWindows();
        }
    }
}
