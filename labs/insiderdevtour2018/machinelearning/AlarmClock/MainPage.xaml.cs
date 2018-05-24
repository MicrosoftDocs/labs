using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.Toolkit.Uwp.Helpers.CameraHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AlarmClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer clockTimer;
        private bool alarmOn = true;
        private SolidColorBrush red = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        private SolidColorBrush black = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        private List<string> labels;
        private string expectedEmotion;
        private string detectedEmotion = string.Empty;

        private CNTKGraphModel model;
        private DateTime? expectedEmotionStart;        



        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromMilliseconds(300);
            clockTimer.Tick += Timer_Tick;
            clockTimer.Start();

            labels = new List<string>()
            {
                "Neutral",
                "Happiness",
                "Surprise",
                "Sadness",
                "Anger",
                "Disgust",
                "Fear",
                "Contempt"
            };

            Random random = new Random();
            expectedEmotion = labels[random.Next(labels.Count)];
            EmotionText.Text = $"Show {expectedEmotion} to Dismiss";
 
            camera.FrameArrived += Preview_FrameArrived;
        }

        // Keep alarm on or turn it off
        private void Timer_Tick(object sender, object e)
        {
            TimeText.Text = DateTime.Now.ToString("HH:mm:ss");

            if (alarmOn)
            {
                Alarmbackground.Background = Alarmbackground.Background == black ? red : black;
            }
            else
            {
                Alarmbackground.Background = black;
            }
        }

        // Get last frame and analyze
        private async void Preview_FrameArrived(object sender, FrameEventArgs e)
        {
            camera.FrameArrived -= Preview_FrameArrived;

            await AnalyzeFrame(e.VideoFrame);

            camera.FrameArrived += Preview_FrameArrived;
        }

        private async Task AnalyzeFrame(VideoFrame frame)
        {
            if (!alarmOn)
                return;

            var bitmap = frame.SoftwareBitmap;
            if (bitmap == null)
                return;

            // Analyze the frame
            string detectedEmotion;
            try
            {
                //detectedEmotion = await DetectEmotion(frame);
                detectedEmotion = await DetectEmotion(bitmap);
            }
            catch
            {
                return;
            }

            await ProcessEmotion(detectedEmotion);
        }

        private async Task ProcessEmotion(string detectedEmotion)
        {
            if (!string.IsNullOrWhiteSpace(detectedEmotion))
            {
                TimeSpan? elapsedTime = null;

                if (expectedEmotion.Equals(detectedEmotion, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Set start time of the emotion to now only on the first detection on a row
                    var now = DateTime.Now;
                    expectedEmotionStart = expectedEmotionStart ?? now;
                    elapsedTime = now - expectedEmotionStart;

                    // if the user has been doing the same emotion for over 3 seconds - turn off alarm
                    if (expectedEmotionStart != null && elapsedTime >= TimeSpan.FromSeconds(3))
                    {
                        alarmOn = false;
                    }
                }
                else
                {
                    expectedEmotionStart = null;
                }

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    DetectedEmotion.Text = string.Format("Detected {0} ({1})", detectedEmotion, elapsedTime.HasValue ? elapsedTime.Value.Seconds : 0);
                });
            }
            else
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    DetectedEmotion.Text = "Face not detected";
                });

                expectedEmotionStart = null;
            }
        }

        private async Task<string> DetectEmotion(SoftwareBitmap image)
        {
            // Set correct subscriptionKey and API Url.
            string subscriptionKey = "2d9c501234824fc1a6be8beac69e6bb2";
            string apiBaseUrl = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0";

            using (InMemoryRandomAccessStream imageStream = new InMemoryRandomAccessStream())
            {
                SoftwareBitmap bitmap = SoftwareBitmap.Convert(image, BitmapPixelFormat.Rgba16);
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, imageStream);
                encoder.SetSoftwareBitmap(bitmap);
                var ratio = bitmap.PixelHeight / 200;
                encoder.BitmapTransform.ScaledHeight = (uint)Math.Round((double)bitmap.PixelHeight / ratio);
                encoder.BitmapTransform.ScaledWidth = (uint)Math.Round((double)bitmap.PixelWidth / ratio);
                await encoder.FlushAsync();

                imageStream.Seek(0);

                var faceServiceClient = new FaceServiceClient(subscriptionKey, apiBaseUrl);

                Face[] faces = await faceServiceClient.DetectAsync(imageStream.AsStream(), false, true, new FaceAttributeType[] { FaceAttributeType.Emotion });
                var detectedFace = faces?.FirstOrDefault();
                return detectedFace == null ? null : detectedFace.FaceAttributes.Emotion.ToRankedList().FirstOrDefault().Key;
            }
        }

        private async void InitializeModel()
        {
            string modelPath = @"ms-appx:///Assets/FER-Emotion-Recognition.onnx";
            StorageFile modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(modelPath));
            model = await CNTKGraphModel.CreateCNTKGraphModel(modelFile);
        }

        private async Task<string> DetectEmotion(VideoFrame frame)
        {
            var emotion = await model.EvaluateAsync(new CNTKGraphModelInput() { Input338 = frame });
            var index = emotion.Plus692_Output_0.IndexOf(emotion.Plus692_Output_0.Max());
            string label = labels[index];

            return label;
        }
    }
}
