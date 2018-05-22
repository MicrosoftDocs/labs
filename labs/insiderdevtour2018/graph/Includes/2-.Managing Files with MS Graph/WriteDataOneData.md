# Writing data to one drive
Until now, we have seen how to obtain data through Microsoft Graph, specifically OneDrive.

Now we will see how we can also use Microsoft Grap to save data, in this case we will select a file and upload it to OneDrive through its Microsoft Graph API.

Let's go for it

## Set file in OneDrive from Graph API.

In UWP project go to **OneDriverHelper.cs** UploadItem method and follow the steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code

            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var randomAccessStream = await storageFile.OpenReadAsync();
                
                using (var contentStream = randomAccessStream.AsStreamForRead())
                {
                    var uploadedItem = await graphClient
                                                 .Drive
                                                 .Root
                                                 .ItemWithPath($"Hol/Graph/{storageFile.Name}")
                                                 .Content
                                                 .Request()
                                                 .PutAsync<DriveItem>(contentStream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error get upload file in OneDrive: " + ex.Message);
                throw;
            }


> **Note:** The file will be created in the **Hol/Graph** folder. 


## Select file & save in one drive

- Build and run the application.

- Click in Log In button to authenticate.

- After the authentication choose ** Upload file to OneDrive ** button.

- Click in ** Select File** button.

- Select file from your computer.

- After select file automatically the file is uploaded to your OneDrive.

- Open your OneDrive and you can see the file in Hol/Graph/ folder

> **Note:** If you want to upload large files visit this [link](https://docs.microsoft.com/en-us/onedrive/developer/rest-api/api/driveitem_createuploadsession).

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/UploadFileOD.png) 
