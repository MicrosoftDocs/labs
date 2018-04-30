# Building a serverless web app

> **Important** 
>
> Log in to Azure using credentials in the steps below. Do not use your own account or create a new one.

In this walkthrough tutorial, you will deploy a simple web application that enables users to upload their images and automatically get captions describing them. The application will present an HTML-based user interface displaying the list of images already uploaded (after signing in), all managed with a serverless backend.

The application uses Blob Storage (both for web static content and images/thumbnails), Azure Functions, Logic Apps, Cosmos DB, Computer Vision, and Azure Active Directory as pictured below:

!IMAGE[architecture.jpg](architecture.jpg)


## What is covered in this lab?

* Create an Azure Storage account and configure containers to host static websites and images 
* Create an Azure Function for uploading images to blob storage
* Resize images using Azure Functions 
* Store image metadata in Cosmos DB
* Use Cognitive Services Vision API to auto-generate image captions
* Add authentication 

