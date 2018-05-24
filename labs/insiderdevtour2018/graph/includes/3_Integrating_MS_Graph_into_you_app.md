# Integrating MS Graph into your app

----------


# Using OneDrive for app data storage

Now that we have learned how to interact with OneDrive, one of the options we have is to save the information of our application in OneDrive: settings, images, documents, backups ..., 

So, if we install the application in another device we can recover all the information and settings we had.

Specifically in this section we are going to simulate how to save settings in OneDrive and how to recover them after a clean installation.

## Write app settings to OneDrive

In UWP project go to **Helpers/DataSyncHelper.cs** SaveSettingsInOneDrive method and follow the steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code

          	try
            {
                var model = JsonConvert.SerializeObject(settingsModel);
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
          
                using (var contentStream = GenerateStreamFromString(model))
                {
                    var uploadedItem = await graphClient
                                                 .Drive
                                                 .Root
                                                 .ItemWithPath($"Hol/Graph/Settings/settings.txt")
                                                 .Content
                                                 .Request()
                                                 .PutAsync<DriveItem>(contentStream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error get upload settings file in OneDrive: " + ex.Message);
                throw;
            }

- Build and run the application.

- Click the Log in button.

- Select the **Save App Data in OneDrive** option in the menu

- Activate all options

- Click the Save Setting button

![alt text](.../media/SaveAppData.png)

- Now go to your OneDrive and you can see the file settings.txt was created in HOL/Graph/Settings. This file contains the app settings.

## Restore settings when installing the app

Now we need to uninstall our UWP app. Search for Microsoft.Graph.HOL App, right click and uninstall.

Now in the UWP project, go to **Helpers/DataSyncHelper.cs** GetSettingsInOneDrive method and follow these steps:

- Delete the code

	`return new SettingsModel();

- Add the following code

            try
            {                
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var settingsStream = await graphClient
                                                .Drive
                                                .Root
                                                .ItemWithPath($"Hol/Graph/Settings/settings.txt")
                                                .Content
                                                .Request()
                                                .GetAsync();

                var settingsString = DeserializeFromStream(settingsStream);
                return JsonConvert.DeserializeObject<SettingsModel>(settingsString);
            }
            catch(Microsoft.Graph.ServiceException ex)
            {
                if (ex.Error.Code.Equals("itemNotFound"))
                {
                    return new SettingsModel();
                }
                Debug.WriteLine("Error get upload file in OneDrive: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error get upload file in OneDrive: " + ex.Message);
                throw;
            }



- Build and run the application.

- Click the Log in button.

- Select the **Save App Data in OneDrive** option from the menu.

- You'll see the options you selected before.

# Writing data to the MS Graph

Microsoft Graph is not a closed tool that gives us a series of classes and functions and nothing else.

One of its strengths is the ability to expand that information with personalized information.

In this section we will see how we can do it.

## Add custom data to resource	

In the UWP project go to **Helpers/UserExtensionHelper.cs** SetExtension method and follow these steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code

           try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                var extenion = new OpenTypeExtension
                {
                    ExtensionName = extensionName,
                    AdditionalData = data
                };

                await graphClient.Me.Extensions.Request().AddAsync(extenion);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error to set extension in graph: " + ex.Message);
                throw;
            }

- Build and run the application.

- Click the Log in button.

- Select the **Add User Extension** option in menu

- Enter a name for the extension and a value

- Click on Add Extension.


![alt text](.../media/CustomData.png) 

After saving the extension, the app calls to Graph to obtain the extension and its value and set it again.
You can see the following code in **UserExtension.xaml.cs** 

 	
		private async void Button_AddExtension_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Progress.IsActive = true;
                var dictionary = new Dictionary<string, object>();
                dictionary.Add(this.txtExtension.Text, this.txtExtensionValue.Text);
				//Save the extension
                await UserExtensionHelper.SetExtension(this.txtExtension.Text, dictionary);
                InfoText.Text = "Extension Added Correctly.Get Extensions....";
				//GetExtension
                var extensionList = await UserExtensionHelper.GetOpenExtensionsForMe();
                var rmyExtension = extensionList.Where(x => x.Display.Equals(this.txtExtension.Text)).First();
                await UserExtensionHelper.DeleteOpenExtensionForMe(this.txtExtension.Text);
                InfoText.Text = $"Extension {rmyExtension.Display} with value {rmyExtension.Properties[rmyExtension.Display].ToString()}  Added";
            }
            catch (Exception ex)
            {
                InfoText.Text = $"OOPS! An error ocurred: {ex.GetMessage()}";
            }
            finally
            {
                this.Progress.IsActive = false;
            }
        }

The code to call Microsoft Graph API to get extension are in **Helpers/UserExtensionHelper.cs** GetOpenExtensionsForMe method:

 		public static async Task<List<ExtensionModel>> GetOpenExtensionsForMe()
        {
            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var result = await graphClient.Me.Extensions.Request().GetAsync();

                return result.CurrentPage.Select(r => new ExtensionModel()
                {
                    Display = r.Id,
                    Properties = (Dictionary<string, object>)r.AdditionalData
                }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error to get sextension in graph: " + ex.Message);
                throw;
            }
        }

   
> **Note:** The Maximum number of extensions by applications is two.

# Integrating with office

But not only can we integrate with OneDrive, we can also integrate with the entire Office package through Graph API, in this section we will see how we can interact with Outlook to get contacts and to schedule events in the calendar.

## Get personal contacts from Outlook

In the UWP project go to **Helpers/OutlookHelper.cs** GetContacts method and follow these steps:

- Delete this code

	`throw new NotImplementedException();`

- Add the following code

            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                return await graphClient.Me.Contacts.Request().GetAsync();
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error get contacts files in OneDrive: " + ex.Message);
                throw;
            }

- Build and run the application.

- Click the Log in button.

- Select the **Outlook Contacts** option in the menu

- You can see the name and email from your Outlook contacts

![alt text](.../media/OutlookContacts.png) 

## Create an event in the Outlook calendar 

In the UWP project go to **Helpers/OutlookHelper.cs** SetAppintment method and follow these steps:

- Delete the code

	`throw new NotImplementedException();`

- Add the following code

            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                var start = new DateTimeTimeZone();
                start.DateTime = startCombo.ToString();
                start.TimeZone = TimeZoneInfo.Local.StandardName;

                var end = new DateTimeTimeZone();
                end.DateTime = endCombo.ToString();
                end.TimeZone = TimeZoneInfo.Local.StandardName;

                var evt = new Event()
                {
                    Subject =subject,
                    Start = start,
                    End = end
                };
                await graphClient.Me.Events.Request().AddAsync(evt);
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error set appintmen files in OneDrive: " + ex.Message);
                throw;
            }


- Build and run the application.

- Click the Log in button.

- Select the **Schedule event in Outlook** option from the menu
.
- Add a subject, select a start date and hour and an end date and hour.

- Click on Schedule Event.

![alt text](.../media/EventCalendar.png) 

- If you go to the Outlook calendar, you can see the event.

# Writing to the Timeline

Timeline is a new feature in Microsoft Graph API that stores the state of your application when a person wants to continue those activities across multiple devices.  
To do that, Microsoft Graph API creates a UserActivites that links the user back into the applications.
In this module you will learn how UserActivities work and view the Timeline in Windows.

## Create and Save an Activity in Graph

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


## Create a Session in Graph

Well, now we have saved an activity but we haven't created a session. A user session activity for the user is needed to save the state of the app.

In the code before uncomment the line

	//currentActivity = userActivity.CreateSession();

- Build and run the application.

- Click the Log in button.

- Select **Add in Timeline** option in menu

- Click on Create Activity.

![alt text](.../media/CreateActivity.png) 

Now we go to the task view in windows.

![alt text](.../media/TaskView.png) 


And we can see the applications in Timeline. If we close the app and click on the Timeline app card, the application will be reopened.

![alt text](.../media/Timeline.png) 

> **Note:** For Timeline we use **Adaptive Cards** to show the app in Timeline. You can learn more about **Adaptive Cards** [here](http://adaptivecards.io/).


# Advance app feature

Timeline helps the user to pick up where they left off working. When you create an activity and session, Cortana displays applications to continue working on.

## Pick up where you left off

Close the application and open **Cortana** to see:


![alt text](.../media/Cortana.png) 

If we click the application it opens to the previous state

## Deep Link

At this point, the application opens to the Login init page.
If we want continue working where we left, we need pass arguments to our application.
In the previous module, when we created the Activity we used this line:

 	userActivity.ActivationUri = new Uri($"holmicrosoftgraph://{UserExtensionHelper.option}");

ActivationUri is optional and at this point is always empty. Go to save where we are.

In the UWP project go to **Helpers/UserExtensionHelper.cs** PickupWhereYouLeft method and follow these steps:

- Add the code


			try
            {
                UserExtensionHelper.option = pickUpOption;
                await UserExtensionHelper.CreateActivity();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error to create activity in graph: " + ex.Message);
                throw;
            }

- Build and run the application.

- Click the Log in button.

- Select any option.

- Close the application.

- Open Cortana and go to Pick up where you left off.

- Open the app.

- The app opens where we are.

We can save the state of the application to OneDrive as seen in previous sections.

> **Cross Platform:** Thanks to Microsoft Graph Activities we can open the application in Android or iOS at the same point that left off in our UWP. If you want learn more about how do it visit this [link](https://github.com/Microsoft/project-rome).