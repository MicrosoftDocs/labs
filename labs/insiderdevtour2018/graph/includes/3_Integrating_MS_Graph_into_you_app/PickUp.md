# Advance app feature

Timeline helps the user to pick up where they left off working. When you create an activity and session, Cortana displays applications to continue working on.

## Pick up where you left off

Close the application and open **Cortana** to see:


![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/Cortana.png) 

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