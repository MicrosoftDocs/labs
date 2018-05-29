# Introducing Microsoft Graph
----------

## Brief explanation of what is the Microsoft Graph

The Microsoft Graph is an API built in Office 365 that allows developers to develop more intelligent and productive applications.

Thanks to the Microsoft Graph we can integrate services such as Azure AD, OneDrive, Outlook, Excel, OneNote, Intune, SharePoint or Planner and other Office products.
  
The best thing about the Microsoft Graph is that integration is simple via the REST API, with only a few lines of code giving access to a large amount of information.  

Two primary entities of the Microsoft Graph are users and groups:

- A **user** in the Microsoft Graph is one of millions who use Microsoft 365 cloud services. It is critical that user identity is protected and access is well managed. The user's data is what drives business. The Microsoft Graph services makes this data available to businesses in rich contexts, with real-time updates, deep insights, and always with the appropriate permissions.

- An Office 365 **group** is the fundamental entity that lets users collaborate. It integrates with other services, enabling richer scenarios in task planning, teamwork, education, and more.

In summary, the Microsoft Graph forms a network of Microsoft 365 services and features that manage, protect, and extract data to support a wide range of scenarios. The Microsoft Graph lets you access this wealth of user data while always respecting proper authorization.


## Why is it useful?

The Microsoft Graph, by allowing us to connect with all our Office 365 data, allows us to integrate this data into our applications easily, quickly and safely.

With the Graph we do the following:

 - Sends alerts if you're spending too much time in meetings: [Microsoft Graph API Outlook meetings](https://developer.microsoft.com/en-us/graph/docs/concepts/findmeetingtimes_example)
 - Suggest the best time for the next team meeting according to your calendar: [Microsoft Graph API Outlook Calendar](https://developer.microsoft.com/en-us/graph/docs/concepts/outlook-schedule-recurring-events)
 - Create a Team in Microsoft Teams for an existing office 365 Group:[Microsoft Graph API Teams](https://developer.microsoft.com/en-us/graph/docs/api-reference/beta/resources/group)
 - Send a notification when people are added to Active Directory and automatically kick off employee on-boarding workflows:[Microsoft Graph API Azure AD](https://docs.microsoft.com/en-US/azure/active-directory/develop/active-directory-graph-api)
 - Continue editing a document where you left off in another device :[Microsoft Graph API Cross-Device](https://developer.microsoft.com/en-us/graph/docs/concepts/cross-device-app-configuration)
 - Create a timeline with all your activity:[Microsoft Graph API Activities](https://developer.microsoft.com/en-us/graph/docs/concepts/activity-feed-concept-overview)
   
These are some examples of what can be done with the Microsoft Graph, but you can see there are many scenarios where you can apply.

Nowadays one of the biggest uses of the Graph API is to share information between different devices, allowing us to save the information of our applications, their status, their configuration and to be able to follow exactly the same point in another device with an application developed in a different technology.

## Interesting links

There is a wealth of information about the Microsoft Graph and this is a list of interesting links we recommend:

- [https://developer.microsoft.com/en-us/graph/docs/concepts/overview](https://developer.microsoft.com/en-us/graph/docs/concepts/overview). Official website of the Microsoft Graph where you can find all the updated documentation.

- [https://github.com/microsoftgraph](https://github.com/microsoftgraph). Microsoft Graph API Repository on GitHub. 

- [https://github.com/Microsoft/project-rome](https://github.com/Microsoft/project-rome). Project Rome's GitHub Repository, where you can find information and examples.

- [https://channel9.msdn.com/events/Build/2017/B8025](https://channel9.msdn.com/events/Build/2017/B8025). Microsoft Build 2017 video showing how to work with Project Rome and Microsoft Graph