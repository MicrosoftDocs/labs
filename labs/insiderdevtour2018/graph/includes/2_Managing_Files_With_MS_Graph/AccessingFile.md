# Accessing file data

What we are going to do in this module is to retrieve the list of the most recent OneDrive documents, building on the knowledge acquired in the Sample API Calls Module.


## Get my recent files from OneDrive from Graph API

In UWP project go to **Helpers/OneDriverHelper.cs** GetRecentItems method and follow these steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code

         	List<DriveItem> filesName = new List<DriveItem>();

            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var onedrive = graphClient.Me.Drive.Recent().Request().GetAsync().Result;
                filesName = onedrive.Take(10).ToList();
                return filesName;
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error get recent files in One Drive: " + ex.Message);
                throw;
            }

After getting graphClient we can access to OneDrive via graphClient.Me.Drive and via the Recent Method get the most recent files.


- Build and run the application.
- Click the Log in button to authenticate.
- After the authentication appears a **Recent File OneDrive** button. Click on this button.
- The application displays the first ten documents that have been modified recently

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/RecentFiles.png) 
