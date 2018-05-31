### Microsoft Graph API use and purpose
Microsoft Graph is a central API in Office 365 that provides unified access to user resource, relationships and intelligence via a single REST endpoint.

### Security and permissions
- All results are scoped to the context of the current user.
- All applications using Graph API must be granted access by the user or tenant administrator.

### Tour of Graph API calls
| Operation        | URL           |
| ------------------------ |:-------------:| 
| GET my profile	      | https://graph.microsoft.com/v1.0/me |
| GET my files	      | https://graph.microsoft.com/v1.0/me/drive/root/children |
| GET my photo	      | https://graph.microsoft.com/v1.0/me/photo/$value |
| GET my calendar events	      | https://graph.microsoft.com/v1.0/me/events |
| GET my manager	      | https://graph.microsoft.com/v1.0/me/manager |
| GET users in my organization	      | https://graph.microsoft.com/v1.0/users |
| GET people related to me	      | https://graph.microsoft.com/v1.0/me/people |
| GET items trending around me	      | https://graph.microsoft.com/beta/me/insights/trending |
| GET my tasks assigned to me across plans	      | https://graph.microsoft.com/v1.0/me/planner/tasks/ |
| POST Create a new task	      | https://graph.microsoft.com/v1.0/planner/tasks |
| GET data from my Excel file	      | https://graph.microsoft.com/v1.0/me/drive/items/{id}/workbook/ |
| POST new row to my Excel file	      | https://graph.microsoft.com/v1.0/me/drive/root:/demo.xlsx:/workbook/tables/Table1/rows/add |

### Graph Explorer
1.	In a web browser, navigate to https://developer.microsoft.com/graph/graph-explorer
2.	Click on the “Run Query” button

![Graph 1](../media/graph-1.png)

3.	A sample JSON Response will be displayed below.
4.	Click on the button “Sign in with Microsoft” to log in to your Office 365 tenant from the previous exercise.  Take note of the permissions requested in the consent screen.

![Graph 2](../media/graph-2.png)

5.	Click on “show more samples” and activate the “Groups” sample category.

![Graph 3](../media/graph-3.png)

6.	Click on the “GET all groups I belong to” link.  You should receive a permissions error.

![Graph 4](../media/graph-4.png)

7.	This is Graph API’s security protecting the user from unauthorized requests.  During the initial consent screen, no “Group” permissions were requested or granted so the query about Groups is denied.

8.	Click on “modify permissions” and scroll down to activate the “Directory.Read.All”  permission. Click “Modify Permissions” to save.

![Graph 5](../media/graph-5.png)

9.	The session is logged out and a new consent screen is presented with the new permission request.

![Graph 6](../media/graph-6.png)

10.	Upon returning to the Graph Explorer screen, select “all groups I belong to” and the last JSON item will show the Team recently created.
 
![Graph 7](../media/graph-7.png)