# Accessing content of files

In this module we will see how we can download **OneDrive** documents with **Microsoft Graph API ** and we will also see how we can download **OneDrive** documents in **another format** of the original.

## Download content file from OneDrive

In UWP project go to **OneDriverHelper.cs** DownloadFile method and follow the steps:

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

- Click in Log in button to Download File. Show the 10 first items in OneDrive.

- Select **Download file from OneDrive** option in menu

- Select the file and click download. The file will be save in Pictures Folder.

- Go to Pictures Folder and see that the file is there and open it.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/DownloadFile.png) 

## Convert content from OneDrive file

In UWP project go to **OneDriverHelper.cs** ConvertContetPDF method and follow the steps:

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



> **NOTE:** First validate that extension is correct to do the conversion. Not all extension can be converted, in our example convert the document to pdf, to see all the conversions visit this [link](https://docs.microsoft.com/en-us/onedrive/developer/rest-api/api/driveitem_get_content_format).

- Build and run the application.

- Click in Log in button to Download File. Show the 10 first items in OneDrive.

- Select **Convert content from OneDrive** option in menu

- Select a file with one of this extensions: doc, docx, epub, eml, htm, html, md, msg, odp, ods, odt, pps, ppsx, ppt, pptx, rtf, tif, tiff, xls, xlsm, xlsx

- Select the file and click convert and download. The file will be save in **Pictures Folder** wiht the same name but with pdf extension.

- Go to Pictures Folder and see that the file is there and open it.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/ConvertFile.png) 
