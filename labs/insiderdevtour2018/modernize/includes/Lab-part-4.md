# Windows 10 APIs

Prerequisites:

- This lab has a dependency on task **Distribution and versioning** as some Windows 10 APIs require a package identity.

## Send a local toast notification from WPF  app
Desktop apps can send interactive toast notifications just like Universal Windows Platform (UWP) apps. However, there are a few special steps for desktop apps due to the different activation schemes.

### 1. Enable the Windows 10 SDK

First thing that we have to do is enable the Windows 10 SDK for our app.
Right click on the project Microsoft.Knowzy.WPF and select **Unload Project**

![](../media/picture9.jpg)

Then right click our project again, and select Edit Microsoft.Knowzy.WPF.csproj

![](../media/picture10.jpg)

Below the existing ````<TargetFrameworkVersion>```` node, add a new ````<TargetPlatformVersion>```` node specifying our min version of Windows 10 supported

```xml
    <AssemblyName>Microsoft.Knowzy.WPF</AssemblyName>
    <TargetFrameworkVersion>v4.6.2</TargetFrameworkVersion>
    <TargetPlatformVersion>10.0.10240.0</TargetPlatformVersion>
```

### 2. Reference the APIs

Open the Reference Manager (right click project, select Add -> Reference), and select Windows -> Core and include the following references:

- Windows.Data
- Windows.UI

![](../media/picture11.jpg)


### 3. Copy compat library code

The compat library abstracts much of the complexity of desktop notifications. Copy the  <a href="https://raw.githubusercontent.com/WindowsNotifications/desktop-toasts/master/CS/DesktopToastsApp/DesktopNotificationManagerCompat.cs">DesktopNotificationManagerCompat.cs</a> file from GitHub into our project, no matter the location, in our case will place the new class on the root.

**The following instructions require the compat library.**

### 4. Implement the activator

we must implement a handler for toast activation, so that when the user clicks on your toast, our app can do something. This is required for our toast to persist in Action Center (since the toast could be clicked days later when our app is closed). This class can be placed anywhere in our project.

Extend the NotificationActivator class and then add the three attributes listed below, and create a unique GUID for your app

````csharp
[ClassInterface(ClassInterfaceType.None)]
[ComSourceInterfaces(typeof(INotificationActivationCallback))]
[Guid("replaced-with-your-guid-C173E6ADF0C3"), ComVisible(true)]
public class MyNotificationActivator : NotificationActivator
{
    public override void OnActivated(string invokedArgs, NotificationUserInput userInput, string appUserModelId)
    {
        // TODO: Handle activation
    }
}
````

### 5. Register with notification platform

We must register with the notification platform

 In the Package.appxmanifest created on the previous lab:

 - Add declaration for **xmlns:desktop**
 - In the **IgnorableNamespaces** attribute, add ***desktop*** value

````xml
<Package ... xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10" IgnorableNamespaces="... desktop ">
````
- **desktop:Extension** for windows.toastNotificationActivation to declare your toast activator

````xml
<Application>
     ...
     <Extensions>
       <!--Specify which CLSID to activate when toast clicked-->
       <desktop:Extension Category="windows.toastNotificationActivation">
         <desktop:ToastNotificationActivation ToastActivatorCLSID="replaced-with-your-guid-C173E6ADF0C3" />
       </desktop:Extension>

     </Extensions>
   </Application>
````

### 6. Register COM activator

We must register our notification activator type, so that we can handle toast activations.

In our app's startup code, call the following **RegisterActivator** method, passing in our implementation of the **NotificationActivator** class we created in step #4. This must be called in order for us to receive any toast activations.

````csharp
// Register COM server and activator type
DesktopNotificationManagerCompat.RegisterActivator<MyNotificationActivator>();
````

### 7. Send a notification

Sending a notification is identical to UWP apps, except that we will use the **DesktopNotificationManagerCompat** class to create a **ToastNotifier**.

If we want to construct notifications using C# instead of using raw XML, we need to install the Notificacion Library **Microsoft.Toolkit.Uwp.Notification**

![](../media/picture12.jpg)

For this exercise we are going to send a toast notification when a user create or update an item. On **EditItemViewModel.cs** file:

- Add the references that we'll need

````csharp
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
````

- Create a function that will send the toast notification

````csharp
       public void SendToastNotificacion()
       {
           // Construct the visuals of the toast (using Notifications library)
           ToastContent toastContent = new ToastContent()
           {
               // Arguments when the user taps body of toast
               Launch = "action=viewConversation&conversationId=5",

               Visual = new ToastVisual()
               {
                   BindingGeneric = new ToastBindingGeneric()
                   {
                       Children =
                       {
                         new AdaptiveText()
                          {
                              Text = "An item has been created/updated",
                              HintMaxLines = 1
                          },
                          new AdaptiveText()
                          {
                              Text = "Microsoft.Knowzt.WPF " + DateTime.Now.ToShortTimeString(),
                          }
                       }
                   }
               }
           };

           // Create the XML document (BE SURE TO REFERENCE WINDOWS.DATA.XML.DOM)
           var doc = new XmlDocument();
           doc.LoadXml(toastContent.GetContent());

           // And create the toast notification
           var toast = new ToastNotification(doc);

           // And then show it
           DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
       }
````

- Let's call our new function at the end of the function **SaveAndCloseEditWindow**


### 8. Deploying and debugging

As mention at the beginning  of this lab, some of the Windows 10 APIs require a package identity so in order for us to debug the toast notifications, we should do it over the **PackagingProject**.

![](../media/picture13.jpg)

 Once the app is running, we create or edit an item. An a toast notification will appear.

![](../media/picture14.jpg)

Once the notification disappears, we can still see the notification in **Action Center**

![](../media/picture15.jpg)




## More Information

If you want to see other visualizations of Toast notifications, please visit this <a href="https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/adaptive-interactive-toasts">link</a>
