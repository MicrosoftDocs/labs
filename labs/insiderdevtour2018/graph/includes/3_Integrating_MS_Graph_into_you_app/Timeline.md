# Writing to the Timeline

Timeline is a new feature in Microsoft Graph API that stores the state of your application when a person wants to continue those activities across multiple devices.  
To do that, Microsoft Graph API creates a UserActivites that links the user back into the applications.
In this module you will learn how UserActivities work and view the Timeline in Windows.

## Create and Save an Activity in Windows 10

In UWP project go to **Helpers/UserExtensionHelper.cs** CreateActivity method and follow the steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code
       	
            try
            {           
                UserActivitySession currentActivity;
                UserActivityChannel channel = UserActivityChannel.GetDefault();

                UserActivity userActivity = await channel.GetOrCreateUserActivityAsync("HOLMicroGraph");

                var adaptiveCard = File.ReadAllText($@"{Package.Current.InstalledLocation.Path}\AdaptiveCard.json");
                adaptiveCard = adaptiveCard.Replace("{{backgroundImage}}", "https://cdn.graph.office.net/prod/GraphDocuments/en-us/concepts/images/microsoft_graph.png");
                adaptiveCard = adaptiveCard.Replace("{{name}}", "Hands-on Lab Microsoft Graph");
                userActivity.VisualElements.DisplayText = "HOL Microsoft Graph";
                userActivity.ActivationUri = new Uri($"holmicrosoftgraph://{UserExtensionHelper.option}");
                userActivity.VisualElements.Content = AdaptiveCardBuilder.CreateAdaptiveCardFromJson(adaptiveCard);

                await userActivity.SaveAsync();
                //currentActivity = userActivity.CreateSession();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error to create activity in graph: " + ex.Message);
                throw;
            }        


## Create a Session in Windows 10

Well, now we have saved an activity but we haven't created a session. A user session activity for the user is needed to save the state of the app.

In the code before uncomment the line

	//currentActivity = userActivity.CreateSession();

- Build and run the application.

- Click the Log in button.

- Select **Add in Timeline** option in menu

- Click on Create Activity.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/CreateActivity.png) 

Now we go to the task view in windows.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/TaskView.png) 


And we can see the applications in Timeline. If we close the app and click on the Timeline app card, the application will be reopened.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/Timeline.png) 

> **Note:** For Timeline we use **Adaptive Cards** to show the app in Timeline. You can learn more about **Adaptive Cards** [here](http://adaptivecards.io/).
