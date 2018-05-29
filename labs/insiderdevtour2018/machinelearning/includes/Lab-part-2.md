# Emotion detection with Face API

In this section you will create a Machine Learning powered Alarm Clock. Your application will display an alarm ask you to show an emotion on your webcam. The alarm will stay on until you show the requested emotion for at least 3 seconds.

Emotion detection will be implemented using the Azure Cognitive Services Face API


# 1. Create the Clock application
Let´s start creating a simple application displaying a clock.

Open your Visual Studio and create a new UWP application. Name it AlarmClock.

Set the target and minimum versions to Windows 10, version 1803 (10.0; Build 17134), since some of the latest features will be required.

Set the platform to x64, since some of the packages you'll be using do not run under x86

Open Package.appxmanifest file, go to Capabilities section and ensure WebCam option is checked.

Add the following nuget packages to your application:
- Microsoft.Toolkit.Uwp.UI.Controls (v3.0.0)
- Microsoft.ProjectOxford.Face (v1.4.0)

Open the MainPage.xaml and add the following namespace to the Page node:

    xmlns:controls="using:Microsoft.Toolkit.Uwp.UI.Controls"

Now, replace the Grid node by the one below. This grid has 4 rows, containing:
- The webcam preview
- A message with the emotion required to stop the alarm
- A message with the currently detected emotion
- The clock, that will blink and show a little message while the alarm is on

```
    <Grid Background="Black">
        <Grid.RowDefinitions>
            <RowDefinition Height="*"></RowDefinition>
            <RowDefinition Height="60"></RowDefinition>
            <RowDefinition Height="60"></RowDefinition>
            <RowDefinition Height="250"></RowDefinition>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"></ColumnDefinition>
            <ColumnDefinition Width="100"></ColumnDefinition>
        </Grid.ColumnDefinitions>
        <controls:CameraPreview Grid.Row="0" Grid.ColumnSpan="2" x:Name="camera" />
        <TextBlock Grid.Row="2" Grid.ColumnSpan="2" x:Name="DetectedEmotion" VerticalAlignment="Center" HorizontalAlignment="Center" Foreground="White" FontSize="30"></TextBlock>
    </Grid>
```

Now open the MainPage.xaml.cs file.

Replace the current using clauses with the ones below.

Note the first two Microsoft.ProjectOxford.Face and Microsoft.ProjectOxford.Face.Contract. Those are used to access the Cognitive Services Face API. We will go back to that later.

    using Microsoft.ProjectOxford.Face;
    using Microsoft.ProjectOxford.Face.Contract;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;
    using System.Timers;
    using Windows.Graphics.Imaging;
    using Windows.Media;
    using Windows.Media.FaceAnalysis;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Navigation;

# 2. Ask for an emotion
The Face API can detect 8 different emotions ("Neutral", "Happiness", "Surprise", "Sadness", "Anger", "Disgust", "Fear" and "Contempt").

Add some global variables:

    private List<string> labels;
    private string expectedEmotion;
    private string detectedEmotion = string.Empty;
    private VideoFrame lastFrame;

Override the OnNavigatedTo method:

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
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
    }

# 3. Capture frames
Now, let's capture images from the webcam, to later use them for the emotion detection part.

The CameraPreview FrameArrived event will trigger when a new frame is available. So you can use that to capture it.

At the end of the OnNavigatedTo method, add:

    await camera.StartAsync();
    camera.CameraHelper.FrameArrived += Preview_FrameArrived;


And now add the code to pick the frames. For now, we'll simply store the last frame captured

    private async void Preview_FrameArrived(object sender, FrameEventArgs e)
    {
        lastFrame = e.VideoFrame;
    }

    private async Task AnalyzeFrame()
    {
        // Analyze the last frame
        try
        {
            detectedEmotion = await DetectEmotionWithCognitiveServices();
        }
        catch
        {
            return;
        }
    }

# 4. Detect emotion using Cognitive Services

The Face API receives an image and returns the result of the face detection, including the emotion. You can provide the image as a URL or as a Stream. In this section we will do the later.

Also, to speed up the process, we will scale down the image to 200 pixels height, although the API does not really need that.

The code below receives the frame you have captured, scales it down, and returns the emotion detected by Cognitive Services.

You need to provide a valid Subscription Key and API endpoint, we will see how to get them on the next section.

    private async Task<string> DetectEmotionWithCognitiveServices()
    {
        var originalBitmap = lastFrame.SoftwareBitmap;
        if (originalBitmap == null)
            return "No frame captured";

        // Set correct subscriptionKey and API Url.
        string subscriptionKey = "YOUR SUBSCRIPTION KEY";
        string apiBaseUrl = "YOUR ENDPOINT HERE";

        using (InMemoryRandomAccessStream imageStream = new InMemoryRandomAccessStream())
        {
            SoftwareBitmap bitmap = SoftwareBitmap.Convert(originalBitmap, BitmapPixelFormat.Rgba16);
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, imageStream);
            encoder.SetSoftwareBitmap(bitmap);
            var ratio = bitmap.PixelHeight / 200;
            encoder.BitmapTransform.ScaledHeight = (uint)Math.Round((double)bitmap.PixelHeight / ratio);
            encoder.BitmapTransform.ScaledWidth = (uint)Math.Round((double)bitmap.PixelWidth / ratio);
            await encoder.FlushAsync();

            imageStream.Seek(0);

            var faceServiceClient = new FaceServiceClient(subscriptionKey, apiBaseUrl);
            var detectedEmotion = string.Empty;

            try
            {
                Face[] faces = await faceServiceClient.DetectAsync(imageStream.AsStream(), false, true, new FaceAttributeType[] { FaceAttributeType.Emotion });
                var detectedFace = faces?.FirstOrDefault();
                detectedEmotion = detectedFace == null ? "Nothing" : detectedFace.FaceAttributes.Emotion.ToRankedList().FirstOrDefault().Key;
            }
            catch (FaceAPIException e)
            {
                detectedEmotion = "API error. Check the values of subscriptionKey and apiBaseUrl";
            }

            return detectedEmotion;
        }
    }

At the top of the class, add a new global variable:

        private string detectedEmotion = string.Empty;

At the end of AnalyzeFrame method, add the following code:

    // Analyze the frame
    string detectedEmotion;
    try
    {
        detectedEmotion = await DetectEmotion(bitmap);
    }
    catch
    {
        return;
    }

This will run the previously created DetectEmotion method with the last captured frame. You can see that for simplicity, we are ignoring any error.

The last step is to make this evaluation happen regularly. In order to avoid issues with the service, we'll limit the API calls to one every 5 seconds.

At the top of the class, add a new global variable:

        private Timer stopwatch = new Timer(5000);

At the end of the OnNavigatedTo method, add the following code:

    timer.Elapsed += Timer_Elapsed;
    timer.Start();

And the add the code to handle the Elapsed event

    private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        await AnalyzeFrame();

        await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                DetectedEmotion.Text = string.Format("Detected {0} at {1}", detectedEmotion, DateTime.Now.ToLongTimeString());
            }
            );
    }

When the timer fires every 10 seconds, the last frame stored will be evaluated, and the DetectedEmotion TextBlock will be updated with the results.

## 5. Get you account key and API url
Open a browser window and go to https://azure.microsoft.com/services/cognitive-services/.

Click on "Try Cognitive Services for free" and look for Face API in the next page.

<img src="../media/Picture1.png" width="600">

Click on "Get API Key". Accept the service conditions and log in with your preferred account.

<img src="../media/Picture2.png" width="600">

Copy any of the displayed keys to your clipboard and paste it in the DetectEmotion method.

    string subscriptionKey = "YOUR SUBSCRIPTION KEY";

Copy the API endpoint to your clipboard and paste it in the DetectEmotion method.

    string apiBaseUrl = "YOUR ENDPOINT HERE";

You can now run the application and check how detectedEmotion value changes with your expression.
