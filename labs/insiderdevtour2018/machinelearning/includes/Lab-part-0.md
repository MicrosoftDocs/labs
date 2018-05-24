# What will this lab cover

### ML as a service

As well as other offerings in Azure under the paradigm of PaaS (Platform as a Service), Machine learning as a Service (MLaaS) is an array of services that provide machine learning tools as part of cloud computing services. MLaaS helps clients benefit from machine learning without the cost, time and risk of establishing an inhouse internal machine learning team. Infrastructural concerns such as data pre-processing, model training, model evaluation, and ultimately, predictions, can be mitigated through MLaaS.

### WinML

Windows ML is a platform that evaluates trained machine learning models on Windows 10 devices, allowing developers to use machine learning within their Windows applications, including interesting capabilities such as:

- Hardware acceleration. On DirectX12 capable devices, Windows ML accelerates the evaluation of Deep Learning models using the GPU. CPU optimizations additionally enable high-performance evaluation of both classical ML and Deep Learning algorithms.
- Local evaluation. Windows ML evaluates on local hardware, removing concerns of connectivity, bandwidth, and data privacy. Local evaluation also enables low latency and high performance for quick evaluation results.
- Image processing. For computer vision scenarios, Windows ML simplifies and optimizes the use of image, video, and camera data by handling frame pre-processing and providing camera pipeline setup for model input.

### Custom models with Custom Vision

The Custom Vision Service is a Microsoft Cognitive Service to build a custom image classifier, simplifying the process of building, deploying and improving an image classifier. The Custom Vision Service provides a REST API and a web interface to upload images and train the classifier.

The Custom Vision Service is designed to work best when the item to classify is prominent in the image. 50 images per class are enough to start prototyping, since the methods used by the Custom Vision Service uses are robust to differences. This also means that the Custom Vision Service is not designed, and will not perform as well, when the differences between images are more subtle, such as minor cracks or dents in a Quality Assurance scenario.

In the Custom Vision section of this HOL you'll create an a custom model to analyze an image, detect which product appears in it, and then show context aware help.

## Prerequisites
- Windows 10 April 2018 update
- <a href="https://developer.microsoft.com/windows/downloads/windows-10-sdk">Windows 10 SDK</a> (Build 17134 or higher)
- <a href="https://developer.microsoft.com/windows/downloads">Visual Studio 15.7+</a>
