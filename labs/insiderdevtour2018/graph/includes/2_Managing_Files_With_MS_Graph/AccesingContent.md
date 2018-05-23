# Accessing file contents

In this module we will see how to download OneDrive documents with Microsoft Graph API and how to download OneDrive documents in a **different format** from the original.

## Download file from OneDrive

In UWP project go to **Helpers/OneDriverHelper.cs** DownloadFile method and follow the steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code

          	try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();                
                return await graphClient.Me.Drive.Items[driveItem.Id].Content.Request().GetAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error download file in OneDrive: " + ex.Message);
                throw;
            }

- Build and run the application.

- Click on the Log in button.

- Select **Download file from OneDrive** option in menu. Show the 10 first items in OneDrive.

- Select the file and click download. The file will be saved in the Pictures Folder.

- Go to the **Pictures Folder** and see that the file is there and open it.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/DownloadFile.png) 

## Convert content from OneDrive file

In UWP project go to **Helpers/OneDriverHelper.cs** ConvertContetPDF method and follow the steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code

            if(!ValidateExtension(driveItem.Name))
            {
                throw new Exception("File extension incorrect. Only accepts doc, docx, epub, eml, htm, html, md, msg, odp, ods, odt, pps, ppsx, ppt, pptx, rtf, tif, tiff, xls, xlsm, xlsx");
            }
            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                List<QueryOption> queryOptionsList = new List<QueryOption>()
                {
                    new QueryOption("format", "pdf")
                };                

                return await graphClient.Me.Drive.Items[driveItem.Id].Content.Request(queryOptionsList).GetAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error convert content in OneDrive: " + ex.Message);
                throw;
            }



> **NOTE:** First validate that the file extension can be converted. Not all extensions can be converted and in our example we convert the document to pdf. To see all the conversions, visit this [link](https://docs.microsoft.com/en-us/onedrive/developer/rest-api/api/driveitem_get_content_format).

- Build and run the application.

- Click on Log in button.

- Select the **Convert content from OneDrive** option in menu.

- Select a file with one of these extensions: doc, docx, epub, eml, htm, html, md, msg, odp, ods, odt, pps, ppsx, ppt, pptx, rtf, tif, tiff, xls, xlsm, xlsx

- Select the file and click convert and download. The file will be saved in the **Pictures Folder** with the same name but with a pdf extension.

- Go to Pictures Folder and see that the file is there and open it.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/ConvertFile.png) 
