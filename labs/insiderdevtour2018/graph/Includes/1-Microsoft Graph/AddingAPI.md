# Adding API calls to your project

Now we have an overview about how work with Microsoft Graph. Now we will back to our UWP and go to add Authentication user.

## Add user authentication with Graph
Open UWP code in Visual Studio and follow this steps.

- Go to AuthenticationHelper.cs
- Delete the code

   	`throw new NotImplementedException();`

- Add the following code

		if (graphClient == null)
            {
                // Create Microsoft Graph client.
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
			}
			return graphClient;


As you can see it is exactly the same code that we used in the console application, and this is because for the UWP application we have used the same NuGet package.

> **Note:** In previous steps we had configured the **ClientID** and **ReturnURL** field in the App.xaml file, check that you have added them correctly.

Now you run the application and click in **Log in** button to authenticate.
