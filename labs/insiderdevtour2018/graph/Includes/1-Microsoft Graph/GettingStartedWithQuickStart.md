# Getting Started with Quick Start
Our applications can use the Microsoft Graph API.
For this we must have an application ID.
For this we must register the application in the Microsoft Application Registry.
We are going to create it. 

> Note: You will need either a [school or work](https://developer.microsoft.com/en-us/office/dev-program) or [Microsoft account](https://signup.live.com/signup?wa=wsignin1.0&ct=1473983465&rver=6.6.6556.0&wp=MBI_SSL&wreply=https://outlook.live.com/owa/&id=292841&CBCXT=out&cobrandid=90015&bk=1473983466&uiflavor=web&uaid=3b7bae8746264c1bacf1db2b315745cc&mkt=EN-US&lc=1033&lic=1)

## Setting up app id
### Register the app in App Registration Portal
First, go to [Microsoft App Registration Portal](https://apps.dev.microsoft.com/)

> **Note:** Login with your  [school or work](https://developer.microsoft.com/en-us/office/dev-program) or [Microsoft account](https://signup.live.com/signup?wa=wsignin1.0&ct=1473983465&rver=6.6.6556.0&wp=MBI_SSL&wreply=https://outlook.live.com/owa/&id=292841&CBCXT=out&cobrandid=90015&bk=1473983466&uiflavor=web&uaid=3b7bae8746264c1bacf1db2b315745cc&mkt=EN-US&lc=1033&lic=1)

After login we need follow the next steps:

 1. Choose Add an app
  
    ![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/AddApplication.png) 
	

	> **Note**: If you signed in with a work or school account, select the **Add an app** button for **Converged applications**.

 2. Enter an app name and click **Create**
	
	![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/RegisterApp.png) 

	> **Note**: After create the page display a list of properties	


 3. Copy the Application Id and save, we will need it later 
	
	![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/ApplicationID.png) 

	> **Note**: We will need the **Application Id** to configure our app.	

 4. Now, we click in Add Platform and select Native Application 	
	
	![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/NativeApplication.png) 

	> **Note**: In our case we select **Native Applicatio**n becuase we will use a **UWP app**

 5. The Built-in redirect URI value has been created automatically. We copy that we'll need it later. 
	
	![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/RedirectUri.png) 
 
 6. Finally, we click in save. 

## Choosing base project

Now we will download the UWP application. We will use it and configure it with the Application Id and the Redirect Uri that we save it before.

#### Download UWP project from Github 

Download project form [here](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/src/Microsoft.GraphBase/) 

#### Build and Debug

Now we configure the app with Application Id and Redirect URI.
Open App.xaml file and add this code

	<Application.Resources>
        <x:String x:Key="ida:ClientID">ENTERYOURCLIENTID</x:String>
        <x:String x:Key="ida:ReturnUrl">ENTERYOURREDIRECTURI</x:String>
    </Application.Resources>

> Change **ENTERYOURCLIENTID** for your Applicatoion Id and **ENTERYOURREDIRECTURI** for de Redreict uri we save before

To run the application you must have the following configuration:

1. Install [Visual Studio Community 2017](https://www.visualstudio.com/vs/) or [Visual Studio Enterprise 2017](https://www.visualstudio.com/vs/).
2. Verify Windows 10 [development mode](https://docs.microsoft.com/windows/uwp/get-started/enable-your-device-for-development#accessing-settings-for-developers) is enabled.
3. Make sure that you've installed the tools for[ Windows 10 development](https://developer.microsoft.com/windows/downloads).
 
To Build and run the applications follow this steps.

1. Now select x86 as build target.
2. Select Local Machine.
3. Build the application.
4. Run the application.

If everything goes fine you will see:

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/applicationrun.png) 
