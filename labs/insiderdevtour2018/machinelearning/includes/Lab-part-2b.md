# Emotion detection with Windows ML


In this section we will add a previously trained model to the project and use it instead of Cognitive Services.

## 1. Use a local model for Emotion detection

Lets start by downloading the model from the <a href="https://gallery.azure.ai/Model/Emotion-recognition-in-faces-FER">Azure AI Gallery</a>.

Drag and drop the FER-Emotion-Recognition.onnx file you have download to the Assets folder in your Solution Explorer.

A new FER-Emotion-Recognition.cs file is created with the necessary code to create and execute the model.

Right-click on the FER-Emotion-Recognition.onnx file on your Solution Explorer and open Properties.

On the Properties panel, set Build Action to "Content" and Copy to Output Directory to "Copy if newer"

Add the last global variable to hold the model you will create

    private CNTKGraphModel model;

Add the method below to Initialize the model.

    private async void InitializeModel()
    {
        string modelPath = @"ms-appx:///Assets/FER-Emotion-Recognition.onnx";
        StorageFile modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(modelPath));
        model = await CNTKGraphModel.CreateCNTKGraphModel(modelFile);
    }

Add a call to InitializeModel at the end of the MainPage constructor.

    public MainPage()
    {
        this.InitializeComponent();
        this.InitializeModel();
    }

Create a new DetectEmotion method using the local model instead of Cognitive Services. Note that, in this case, the VideoFrame obtained from the camera can be used directly.

    private async Task<string> DetectEmotionWithWinML()
    {
        var videoFrame = lastFrame;
        var emotion = await model.EvaluateAsync(new CNTKGraphModelInput() { Input338 = videoFrame });
        var index = emotion.Plus692_Output_0.IndexOf(emotion.Plus692_Output_0.Max());
        string label = labels[index];

        return label;
    }

In the AnalyzeFrame method, replace the call to the previous DetectEmotion with this new one:

    detectedEmotion = await DetectEmotionWithWinML();
    //detectedEmotion = await DetectEmotionWithCognitiveServices();

You can now try the application again.

## 2. Crop the image

In order to improve the detection of the emotion, the Cognitive Services Face API performs an automatic crop. Let's do the same manually for the WinML scenario.

First, add the following global variable

    private FaceDetector faceDetector;

Then, add the following code at the beginning of the DetectEmotionWithML method, after the var videoFrame = lastFrame; line

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

Now try the application again, and the results should improve

## 3. Make the clock work

Sections 3 to 5 are just a little of bells and whistles to make the application look like an actual alarm clock.

Let's add the code to update the clock.

First, go to the MainPage.xaml file and add the following controls before the closing tag of the Grid

    <TextBlock Grid.Row="1" Grid.ColumnSpan="2" x:Name="EmotionText" VerticalAlignment="Center" HorizontalAlignment="Center" Foreground="White" FontSize="30"></TextBlock>
    <TextBlock Grid.Row="3" Grid.Column="0" x:Name="TimeText" VerticalAlignment="Center" HorizontalAlignment="Center" TextAlignment="Center" Margin="100,0,0,0"  Foreground="White" FontSize="200">12:00:00</TextBlock>
    <TextBlock Grid.Row="3" Grid.Column="1" x:Name="AlarmText" VerticalAlignment="Top" HorizontalAlignment="Right" TextAlignment="Right" Margin="0, 10, 10, 0" Foreground="White" FontSize="20">Alarm ON</TextBlock>


Add the following global variable

    private DispatcherTimer clockTimer;

Add the following code at the end of the OnNavigatedTo method

    // Choose Happiness as expected emotion
    expectedEmotion = labels[1];
    EmotionText.Text = $"Show {expectedEmotion} to Dismiss";

    clockTimer = new DispatcherTimer();
    clockTimer.Interval = TimeSpan.FromMilliseconds(300);
    clockTimer.Tick += Timer_Tick;
    clockTimer.Start();

Add also the code for the Timer_Tick handler

    private void Timer_Tick(object sender, object e)
    {
        TimeText.Text = DateTime.Now.ToString("HH:mm:ss");
    }    

## 4. Add the alarm
For simplicity sake, the alarm will start when the application starts, and will keep on until the desired emotion is detected for at least 3 seconds.

Add the following global variables:

    private bool alarmOn = true;
    private SolidColorBrush red = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
    private SolidColorBrush white = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

At the beginning of the AnalyzeFrame method, add the code below to avoid analyzing images if the alarm hasn't triggered

    if (!alarmOn)
        return;

At the end of the Timer_Tick method, add the code below. This will make the background blink from red to black while the alarmOn variable is true.

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

## 5. Stop the alarm when emotion is detected
Finally, we need to stop the alarm when the required emotion (happiness) is detected.

Add the following method to check if the alarm must be turned off:

    private async Task ProcessEmotion(string detectedEmotion)
    {
        if (!string.IsNullOrWhiteSpace(detectedEmotion) && (expectedEmotion.Equals(detectedEmotion, StringComparison.CurrentCultureIgnoreCase)))
            {
            alarmOn = false;
        }
    }

Add a call to ProcessEmotion at the end of the try block in the AnalyzeFrame method:

    try
    {
        detectedEmotion = await DetectEmotionWithWinML();
        await ProcessEmotion(detectedEmotion);
    }


That's all, you can test the application again. The alarm will be dismissed when happiness is detected
