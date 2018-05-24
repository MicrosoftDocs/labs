## Product detection with Custom Vision

In this section you will create an application that uses a model to detect which product appears in an image and show help for the detected product.

## 1. Download the example
Let's start by downloading the <a href="https://aka.ms/winmllab">ContosoIT</a> UWP application and uncompress it in a folder of your choice.

This application allows the user to pick an image of the product from the disk or from the webcam. It already includes the help for two different products, Surface Pro and Surface Studio.

You will need to add intelligence to the application, so it can detect which of those two products is in the provided image.

Open the project using Visual Studio. You can run it if you want to take a look. Note that, no matters which image you pick, it will always detect Surface Pro as the product.

## 2. Create your Custom Vision project
In this section you will create the Custom Vision model you will use in the ContosoIT application.

Open a browser and go to the <a href="https://customvision.ai/projects">Custom Vision</a> site and log in with your account, or create one for free if you don't have one.

Create a new project with the following settings:
- Name: ContosoIT
- Project Types: Classification
- Domains: General (compact)  

Make sure you pick "General (compact)" and not "General", so you can later export the model.

<img src="/Media/Picture3.png" width="700">

## 3. Train your model
In this section you will use some images of each product to train the model. The ContosoIT application you have downloaded already contains a set of images for this purpose.

Click on "Add images" on the menu at the top of the page, and then in "Browse local files".

Go to your ContosoIT application folder and select all images in \resources\training\surface-pro.

Add a "surface-pro" tag and upload the files.

Repeat the process with the images in \resources\training\surface-studio folder, but this time set the tag as "surface-studio"

<img src="/Media/Picture4.png" width="700">

Click Train at the top of the page. The Performance tab will open and show Iteration 1 is in process. Wait until it finishes and your model is ready to test.

## 4. Test the model
Click on "Quick test" on the top of the Performance tab.

Select the image you want to use. You have a couple of images in the \resources\prediction\ folder of your ContosoIT application for this purpose.

A prediction will appear with the probability of each of the two classes supported by the model.

Note that those classes correspond with the tags you set before.

## 5. Export the model
Now, you can export your model so you can integrate it in your application.

Click export on the Performance tab main menu. Several export options will appear.

Choose ONNX, click Export and then Download.

<img src="/Media/Picture5.png" width="700">

## 6. Integrate your model in the ContosoIT application
In this section, you will add your model to your application, and get some code to create an instance of the model and run predictions using C#.

First, as your model file has been downloaded with a automatically generated name, rename it to ContosoIT.onnx

Now, on Visual Studio, ensure the selected platform of your UWP application is x64.

Drag and drop your ContosoIT.onnx file to the Assets folder on your Solution Explorer.

A new ContosoIT.cs file is created with the necessary code to create and execute the model.

Right-click on the ContosoIT.onnx file on your Solution Explorer and open Properties.

On the Properties panel, set Build Action to "Content" and Copy to Output Directory to "Copy if newer"

Review the ContosoIT.cs code to use some more meaningful names. All names starts by two GUIDs, replace them with ContosoIT. Your file should now contain the ContosoITModelInput, ContosoITModelOutput and ContosoITModel classes

## 7. Run the model from your application
There is only one more thing to do. You need to edit the Devices page of your application to make it use your model.

In your Visual Studio project, open Pages/DevicesPage.xaml.cs

Add the following using clause at the top of the file

    using Windows.Storage.Streams;

Add a global variable to contain the model. This way we don't need to initialize a new one for each prediction

    private ContosoIT.ContosoITModel model;

Add a method to Initialize the model. Note the path of your onnx file is used during model creation.

    private async Task InitializeModel()
    {
        string modelPath = @"ms-appx:///Assets/ContosoIT.onnx";
        StorageFile modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(modelPath));
        model = await ContosoIT.ContosoITModel.CreateContosoITModel(modelFile);
    }

Call the method InitializeModel from the OnNavigatedTo event handler, before the BeginDetection call.

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        await InitializeModel();

        var selectedFile = (DetectionDataParametersModel)e.Parameter;
        await BeginDetection(selectedFile);
    }      

Your application is storing the image you pick as a StorageFile. You will need to convert that to a VideoFrame, because that is what the model expect as an input.

To do so, add the following method:

    private async Task<VideoFrame> ImageToVideoframe(StorageFile imageFile)
    {            
        using (IRandomAccessStream imageStream = await imageFile.OpenAsync(FileAccessMode.Read))
        {
            // Create the decoder from the stream
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);

            // Get the SoftwareBitmap representation of the file
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

            VideoFrame imageFrame = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);

            return imageFrame;
        }
    }

And finally, to glue all that together. Look for the comment that says "Your code goes here" and replace the following line:

    var classLabel = "surface-pro";

With these ones:

    ContosoITModelInput modelInput =
        new ContosoITModelInput() { data = await ImageToVideoframe(detectionDataParameters.SelectedFile) };
    ContosoITModelOutput modelResult = await model.EvaluateAsync(modelInput);
    var classLabel = modelResult.classLabel.FirstOrDefault();

Now, the detected product is no longer fixed to "surface-pro", instead, it is the result of the evaluation of your selected image using your classification model.

Use the test images you used before on the Custom Vision web and check the results.

## 8. What's next
That's it, you have finished the Lab. You can go to <a href="https://azure.microsoft.com/overview/machine-learning/">Azure Machine Learning</a> to learn more about Azure's integrated, end-to-end data science environment.
