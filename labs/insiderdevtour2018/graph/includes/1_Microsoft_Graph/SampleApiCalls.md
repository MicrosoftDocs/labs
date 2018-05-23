# Sample API calls

At this point we are ready to interact with Microsoft Graph.

To do this we will create a console application to which we add the necessary code to perform authentication using Microsoft Graph and then we will list all the documents that we have in OneDrive.

## Create a console app

Download the base project from [here](https://github.com/Microsoft/InsiderDevTour18-Labs/tree/master/graph)

## Authenticate user

Now let's add the authentication.

- In App.config add

    	<appSettings>
    		<add key="ida:ClientID" value="YOURCLIENTID"/>
    		<add key="ida:ReturnUrl" value="YOURREDIRECTURI"/>
    	</appSettings>
    
	
>Change the value **YOURCLIENTID** for the Application Id that we obtained when registering the application, and do the same with **YOURREDIRECTURI**


- Go to AuthenticationHelper.cs
- Delete the code

   	`throw new NotImplementedException();`

- Add the following code

	      try
            {
                graphClient = new GraphServiceClient(
                    "https://graph.microsoft.com/v1.0",
                    new DelegateAuthenticationProvider(
                        async (requestMessage) =>
                        {
                            var token = await GetTokenForUserAsync();
                            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);                            

                        }));
                return graphClient;
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Could not create a graph client: " + ex.Message);
            }

            return graphClient;

You can see that we use the Graph Service Client to authenticate, obtain the token, and later access all of Graph resources through the class found in the Microsoft.Graph NuGet package.

The method **GetTokenForUserAsync** obtain the access token after the user are authenticated to send in Authentication header when call the API.

Now you can build and run the process and the application will ask for user credentials via Graph.
When you are authenticated, please answer **N** to the answer **Would you like to see your OneDrive items?[Y] Yes or [N] No**

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/AuthConsola.png)

## Get call to Get all items in OneDrive
Now we are ready to make calls to the API, we will call OneDrive API to show the name of the documents we have.


> **Advice:** the application ask for a number of documents to show. If you don't want to wait a lot, please enter a small number.

For the call to **OneDrive** follow these steps:

- Go to OneDriveHelper.cs.
- Delete the code.
	
	`throw new NotImplementedException();`
-  Add this code:
 			
	       List<string> filesName = new List<string>();

            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                var onedrive = graphClient.Me.Drive.Root.Children.Request().GetAsync().Result;

                filesName = GetNameFiles(graphClient, filesName, onedrive, numberOfElements);
               
                return filesName;
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Could not create a graph client: " + ex.Message);
                throw;
            }

There are few interesting points in the code before:

- We call the authentication method to obtain the Graph context with the authenticated user.
- After that we can access the different GRAPH resources of the user.
- In our case we access the root of OneDrive.
- And then we search for the files by going through all the folders with the recursive GetNameFiles method.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/OneDriveConsole.png)