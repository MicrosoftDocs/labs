# Emotion detection with Face API

In this section, you'll create an alarm clock application that uses machine learning for emotion detection. The application will ask users to show an emotion for at least 3 seconds to turn off the alarm.

Emotion detection will be implemented in two different ways:
- In the intelligent cloud using the Azure Cognitive Services Face API
- On the intelligent edge using a local model with Windows ML

## 1. Create the clock application

Let's start by creating a simple application that displays a clock.

1. Open **Visual Studio**, and go to **File** > **New** > **Project**. Under Visual C#, select Windows Universal Blank App, and name the project AlarmClock.

2. Set the target and minimum versions to Windows 10, version 1803 (10.0; Build 17134).

3. In the Debug menu, set the platform to x64, since some of the packages you'll be using do not run under x86.

4. In the Solution Explorer, open the `Package.appxmanifest` file, go to Capabilities section, and check the WebCam option.

5. In the Solution Explorer, right click on your **Alarm Clock (Universal Windows)** project, and select **Manage Nuget Packages...**. In the Browse tab, search for and install the following nuget packages:
    - Microsoft.Toolkit.Uwp.UI.Controls (v3.0.0)
    - Microsoft.ProjectOxford.Face (v1.4.0)

6. In Solution Explorer, open `MainPage.xaml`. Add the following namespace to the Page:

    ```xaml
    xmlns:controls="using:Microsoft.Toolkit.Uwp.UI.Controls"
    ```

7. Replace the Grid with the code below. This grid has contains:
    - The webcam preview
    - A message with the emotion required to stop the alarm
    - A message with the currently detected emotion
    - The clock, that will blink and show a little message while the alarm is on
    
    ```xaml
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

8. In `MainPage.xaml.cs`, replace the `using` clauses with the ones below.

    ```csharp
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
    ```

## 2. Declare the recognized emotions

In this project, we'll be able to detect 8 different emotions ("Neutral", "Happiness", "Surprise", "Sadness", "Anger", "Disgust", "Fear" and "Contempt").

1. In `MainPage.xaml.cs`, add some global variables:

    ```csharp
    private List<string> labels;
    private string expectedEmotion;
    private string detectedEmotion = string.Empty;
    private VideoFrame lastFrame;
    ```

2. Override the OnNavigatedTo method:

    ```csharp
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
    ```

## 3. Capture frames

Now, let's capture images from the webcam for emotion detection.

1. At the end of the `OnNavigatedTo` method, add:

    ```csharp
    await camera.StartAsync();
    camera.CameraHelper.FrameArrived += Preview_FrameArrived;
    ```

    When a new frame is available, the `CameraPreview.FrameArrived` event triggers, so we can capture the new frame.

2. For now, we'll simply store the last frame captured.

    ```csharp
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
    ```

## 4. Detect emotion using Azure Cognitive Services

The Cognitive Services Face API takes in an image and returns the result of the face detection, including the emotion. You can provide the image as a URL or as a Stream. 

In this project, we'll use a Stream. We'll also scale down the image to 200 pixels height, which the API does not require, but it'll speed up the process.

1. Add the code below to scale down your captured frame, and get the emotion detected by Cognitive Services.

    ```csharp
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
    ```
    
    You need to provide a valid Subscription Key and API endpoint, but we'll see how to get them on the next section.

2. At the top of the class, add a new global variable:

    ```csharp
    private string detectedEmotion = string.Empty;
    ```

3. At the end of AnalyzeFrame method, add the following code:

    ```csharp
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
    ```
    
    This will run the previously created DetectEmotion method with the last captured frame. You can see that for simplicity, we are ignoring any error.
    
4. The last step is to make this evaluation happen regularly. In order to avoid issues with the service, we'll limit the API calls to one every 5 seconds.

    At the top of the class, add a new global variable:
    
    ```csharp
    private Timer stopwatch = new Timer(5000);
    ```
    
    At the end of the OnNavigatedTo method, add the following code:
    
    ```csharp
    timer.Elapsed += Timer_Elapsed;
    timer.Start();
    ```
    
    And then add the code to handle the Elapsed event:
    
    ```csharp
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
    ```
    
    When the timer fires every 10 seconds, the last frame stored will be evaluated, and the DetectedEmotion TextBlock will be updated with the results.

## 5. Get your account key and API url

1. Open a browser window, and go to https://azure.microsoft.com/services/cognitive-services/.

2. Click on "Try Cognitive Services for free", and look for Face API in the next page.

    ![screenshot](../media/Picture1.png)

3. Click on "Get API Key". Accept the service conditions, and log in with your preferred account.

    ![screenshot](../media/Picture2.png)

4. Copy any of the displayed keys to your clipboard and paste it in the DetectEmotion method.
    
    ```csharp
    string subscriptionKey = "YOUR SUBSCRIPTION KEY";
    ```
    
5. Copy the API endpoint to your clipboard, and paste it in the DetectEmotion method.

    ```csharp
    string apiBaseUrl = "YOUR ENDPOINT HERE";
    ```

    You can now run the application, and check how DetectedEmotion changes with your expression.

## 6. Detect emotions with a local model and Windows ML

Now, instead of using Cognitive Services' REST API, we'll add a previously trained model to the project for local evaluation with Windows ML.

1. Download the model from the <a href="https://gallery.azure.ai/Model/Emotion-recognition-in-faces-FER">Azure AI Gallery</a>.

2. In Visual Studio, drag and drop the downloaded `FER-Emotion-Recognition.onnx` file to the Assets folder in your Solution Explorer. Visual Studio will generate a new `FER-Emotion-Recognition.cs` file with the necessary code to create and execute the model.

3. Right-click on the `FER-Emotion-Recognition.onnx` file, select Properties, set Build Action to "Content" and Copy to Output Directory to "Copy if newer".

4. In `MainPage.xaml.cs`, add the last global variable for the model.

    ```csharp
    private CNTKGraphModel model;
    ```

5. Add the method below to initialize the model.

    ```csharp
    private async void InitializeModel()
    {
        string modelPath = @"ms-appx:///Assets/FER-Emotion-Recognition.onnx";
        StorageFile modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(modelPath));
        model = await CNTKGraphModel.CreateCNTKGraphModel(modelFile);
    }
    ```

6. Call InitializeModel at the end of the MainPage constructor.

    ```csharp
    public MainPage()
    {
        this.InitializeComponent();
        this.InitializeModel();
    }
    ```

7. Create a new DetectEmotion method using the local model instead of the Cognitive Services API. Note that, in this case, we can use the VideoFrame from the camera directly.

    ```csharp
    private async Task<string> DetectEmotionWithWinML()
    {
        var videoFrame = lastFrame;
        var emotion = await model.EvaluateAsync(new CNTKGraphModelInput() { Input338 = videoFrame });
        var index = emotion.Plus692_Output_0.IndexOf(emotion.Plus692_Output_0.Max());
        string label = labels[index];
    
        return label;
    }
    ```

8. In the AnalyzeFrame method, replace the call to the previous DetectEmotion with this new one:
    
    ```csharp
    detectedEmotion = await DetectEmotionWithWinML();
    //detectedEmotion = await DetectEmotionWithCognitiveServices();
    ```

    Try running the application again!

## 7. Crop the image

In order to improve the detection of the emotion, the Cognitive Services Face API performs an automatic crop. Let's do the same manually for this scenario.

1. First, add the following global variable:

    ```csharp
    private FaceDetector faceDetector;
    ```
    
2. Then, add the following code at the beginning of the DetectEmotionWithML method.

    ```csharp
    var videoFrame = lastFrame;
    
    if (faceDetector == null)
    {
        faceDetector = await FaceDetector.CreateAsync();
    }
    
    var detectedFaces = await faceDetector.DetectFacesAsync(videoFrame.SoftwareBitmap);
    
    if (detectedFaces != null && detectedFaces.Any())
    {
        var face = detectedFaces.OrderByDescending(s => s.FaceBox.Height * s.FaceBox.Width).First();
        var randomAccessStream = new InMemoryRandomAccessStream();
        var decoder = await BitmapDecoder.CreateAsync(randomAccessStream);
        var croppedImage = await decoder.GetSoftwareBitmapAsync(decoder.BitmapPixelFormat, BitmapAlphaMode.Ignore, new BitmapTransform() { Bounds = new BitmapBounds() { X = face.FaceBox.X, Y = face.FaceBox.Y, Width = face.FaceBox.Width, Height = face.FaceBox.Height } }, ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);
        videoFrame = VideoFrame.CreateWithSoftwareBitmap(croppedImage);
    }
    ```
    
    Now try the application again, and the results should improve!
    
## 8. Make the clock work

Sections 8 to 10 are just a little of bells and whistles to make the application look like an actual alarm clock. If you prefer to skip them, you can go directly to the next project, [Product detection with Custom Vision](#product-detection-with-custom-vision).

Let's add the code to update the clock.

1. In `MainPage.xaml`, add the following controls before the closing `</Grid>` tag.

    ```xaml
        <TextBlock Grid.Row="1" Grid.ColumnSpan="2" x:Name="EmotionText" VerticalAlignment="Center" HorizontalAlignment="Center" Foreground="White" FontSize="30"></TextBlock>
        <TextBlock Grid.Row="3" Grid.Column="0" x:Name="TimeText" VerticalAlignment="Center" HorizontalAlignment="Center" TextAlignment="Center" Margin="100,0,0,0"  Foreground="White" FontSize="200">12:00:00</TextBlock>
        <TextBlock Grid.Row="3" Grid.Column="1" x:Name="AlarmText" VerticalAlignment="Top" HorizontalAlignment="Right" TextAlignment="Right" Margin="0, 10, 10, 0" Foreground="White" FontSize="20">Alarm ON</TextBlock>
    ```

2. In `MainPage.xaml.cs`, add the following global variable.
    
    ```csharp
    private DispatcherTimer clockTimer;
    ```

3. Add the following code at the end of the OnNavigatedTo method.

    ```csharp
    // Choose Happiness as expected emotion
    expectedEmotion = labels[1];
    EmotionText.Text = $"Show {expectedEmotion} to Dismiss";
    
    clockTimer = new DispatcherTimer();
    clockTimer.Interval = TimeSpan.FromMilliseconds(300);
    clockTimer.Tick += Timer_Tick;
    clockTimer.Start();
    ```

4. Add the code for the Timer_Tick handler.

    ```csharp
    private void Timer_Tick(object sender, object e)
    {
        TimeText.Text = DateTime.Now.ToString("HH:mm:ss");
    }
    ```

## 9. Add the alarm

For simplicity, the alarm will start when the application starts and turn off when the desired emotion is detected for at least 3 seconds.

1. Add the following global variables:

    ```csharp
    private bool alarmOn = true;
    private SolidColorBrush red = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
    private SolidColorBrush white = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
    ```

2. At the beginning of the AnalyzeFrame method, add the code below to avoid analyzing images if the alarm hasn't triggered.

    ```csharp
    if (!alarmOn)
        return;
    ```
    
3. At the end of the Timer_Tick method, add the code below. This will make the background blink from red to black while the alarmOn variable is true.

    ```csharp
    if (alarmOn)
    {
        TimeText.Foreground = TimeText.Foreground == white ? red : white;
        AlarmText.Text = "Alarm ON";
    }
    else
    {
        TimeText.Foreground = white;
        AlarmText.Text = "Alarm OFF";
    }
    ```

## 10. Stop the alarm when emotion is detected

Finally, we need to stop the alarm when the required emotion (happiness) is detected.

1. Add the following method to check if the alarm must be turned off:

    ```csharp
        private async Task ProcessEmotion(string detectedEmotion)
        {
            if (!string.IsNullOrWhiteSpace(detectedEmotion) && (expectedEmotion.Equals(detectedEmotion, StringComparison.CurrentCultureIgnoreCase)))
                {
                alarmOn = false;
            }
        }
    ```

2. Add a call to ProcessEmotion at the end of the try block in the AnalyzeFrame method:

    ```csharp
    try
    {
        detectedEmotion = await DetectEmotionWithWinML();
        await ProcessEmotion(detectedEmotion);
    }
    ```

That's it! Run the application again, and the alarm will be dismissed when happiness is detected :)
