# Graph Explorer

Graph Explorer is a tool that allows us to explore and test Microsoft Graph API.

We are going to make an overview to this tool.

## Sign in Graph Explorer

Go to **[Graph Explorer](https://developer.microsoft.com/en-us/graph/graph-explorer)** and click in Sign in Wiht Microsoft button. 

 ![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/LoginGraphApi.png) 

> Note: You will need login with a [school or work](https://developer.microsoft.com/en-us/office/dev-program) or [Microsoft account](https://signup.live.com/signup?wa=wsignin1.0&ct=1473983465&rver=6.6.6556.0&wp=MBI_SSL&wreply=https://outlook.live.com/owa/&id=292841&CBCXT=out&cobrandid=90015&bk=1473983466&uiflavor=web&uaid=3b7bae8746264c1bacf1db2b315745cc&mkt=EN-US&lc=1033&lic=1)

After the login you can see that on the left side we have several options:


- We can modify our permissions


- We can log out


- We can see the APIS, by default we show some services by default but we can add them all by adding APIS from show more samples.

On the right side we have the view with everything we need to launch the calls against the APIS and see the requests and the results that are sent

Now we will see how test Graph API in Microsoft Graph Explorer to get all files in One Drive and how we can manage our pemrisions.

## Modify user premissions

In order to use and access the different services, we can modify the permissions of our user to give him the necessary privileges to be able to use the desired operations.

- Choose Modify Permissions

 ![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/GEModifyPermissions.png) 


- Now we can see the list of permissions and you can add or remove it.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/GESelectPermissions.png) 


## Acitvate OneDrive in Sample Categories

Now we are going to add the OneDrive APIS to Graph Explorer to be able to call this services


- Choose show more samples.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/GEShowMoreExamples.png) 

- Find OneDrive and activate. 

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/GESelectOneDrive.png) 


- Now OneDrive APIS are included in Graph Explorer.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/GEShowOneDriveSamples.png) 


# Get all item in my drive

We are ready to get all items from OneDrive

- Select in left menu **all the items in my drive** under **OneDrive** section
- Automatically Graph Explorer send call to OneDrive API and show the results.

![alt text](/labs-pr/Drive-user-engagement-across-all-your-devices-with-Microsoft-Graph/media/GEGetAllODItems.png) 
