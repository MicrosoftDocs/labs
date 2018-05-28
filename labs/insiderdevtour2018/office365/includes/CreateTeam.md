# Foundations of Office 365 #
## Prerequisites (for Windows, MacOS and Linux) ##
1. [Microsoft Office 365 Tenant configured for Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/get-started/get-started-tenant)
2. Azure subscription with owner permissions. You can create a [free one-month trial](https://azure.microsoft.com/en-us/free/) or [purchase an Azure subscription](https://azure.microsoft.com/en-us/pricing/purchase-options/).
3. [Node LTS and npm](https://nodejs.org/en/)
4. [Visual Studio Code](https://code.visualstudio.com/)
5. Chrome, Firefox, Edge, or IE11 browser.  Teams does not support Safari at this time but the desktop client is available for MacOS.


## Microsoft Teams and more ##

### What is Teams? ###

Teams is the next-generation team collaboration tool in the Office 365 suite. By combining new and existing apps and services, users can find all relevant team content in a single application.

Out of the box it includes:
* Audio and video conferencing 
* Permanent chat
* Online meetings and broadcasts
* Shared files in SharePoint
* Team notes in OneNote
* Team tasks in Planner
* Shared email inbox, calendar and email address
* Available for web, desktop and mobile


### Extending Teams ###

In addition to the built-in components, Teams has several ways to incorporate external content.

### Tabs ###

* Planner
* Power BI
* Trello
* Your custom Tab


### Bots ###
* Who bot
* Your custom Bot

### Connectors ###
* Twitter
* JIRA
* Salesforce
* Your custom connector


### Messaging extensions ###
* Youtube
* Your custom extension

## Create a Team ##

### Prerequisites ###
1. [Microsoft Office 365 Tenant configured for Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/get-started/get-started-tenant)

> **IMPORTANT:** Be sure to enable external apps for Teams and sideloading as indicated in the guide for later exercises.

![Configure tenant for Teams](../media/configure-tenant-teams.png)

### Steps ###

> **Note:** this tutorial is run from a web browser but the desktop experience is nearly identical.

1. Browse to https://teams.microsoft.com and login with your Office 365 account.

![Teams intro 1](../media/teams-intro-1.png)
	
2. Click on “Join or create a team” in the bottom left corner

![Teams intro 2](../media/teams-intro-2.png)

3. Enter the team name as “Lunch”, description and access policy.

![Teams intro 3](../media/teams-intro-3.png)  

> **Note:** Choose the name carefully as this will become the team’s globally visible email address.

![Teams intro 4](../media/teams-intro-4.png)

4. Skip the invitation screen.
5. Congratulations on creating your first Team!
6. In your newly created “Lunch” team, write your first post on the General channel.
7. Each Team has a default General channel but adding more channels is a good way to focus content.  Create a “Cafeteria” channel in the Lunch team.

![Teams intro 5](../media/teams-intro-5.png)

8. Download this [PDF menu](https://github.com/Microsoft/InsiderDevTour18-Labs/blob/master/office365/cafe_menu.pdf) to your **Downloads** folder. 

9. In the Cafeteria channel, click on Files and upload the PDF menu file from the **Downloads** folder.

![Teams intro 6](../media/teams-intro-6.png)

10. Click on the ellipsis menu (…) to the right of the file name and choose “Make this a tab”.

![Teams intro 7](../media/teams-intro-7.png)

11. Click on the + button in the tab section to add another tab.

![Teams intro 8](../media/teams-intro-8.png)

12. Now that you have seen Teams from a user's perspective, read on to see what a developer can do with Microsoft Graph API!