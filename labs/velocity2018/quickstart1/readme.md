# Quick Start 1: Azure DevOps Project

The Azure DevOps Project simplifies the initial configuration of a continuous integration (CI) and continuous delivery (CD) pipeline to deploy your app on Azure in a few quick step. It sets you up with everything you need for developing, deploying, and monitoring your app.


## Exercise 1: Setting up Sample Java project using Azure DevOps Project

1. Sign into the [Microsoft Azure portal](https://portal.azure.com).

1. Choose the **+ Create a resource** icon in the left navigation bar, then search for **DevOps project**. Then choose **DevOps Project** in the list and click on **Create**

    ![searchdevopsproject](images/searchdevopsproject.png)

1. Select the **Java** sample application and click on **Next**. The Java samples include a choice of several application frameworks.

   ![selectjava](images/selectjava.png)

1.  Select the default sample framework is **Spring**. choose **Next**.

1. Select **Web App For Containers** and choose **Next**.

1. Finally, you will need to provide a Team Services account. Since you do not have one, you will create a new VSTS account. Provide a name for your VSTS **account** and **project** and choose **Done**.

    ![vstsproject](images/vstsproject.png)

7. In a few minutes, the **project dashboard** loads in the Azure portal. A sample java application is set up in a repository in your VSTS account, a build executes, and your application deploys to Azure. This dashboard provides visibility into your **code repository**, **VSTS CI/CD pipeline**, and your **application in Azure**. 
   ![dashboard](images/dashboard.png)

8.  On the right side of the dashboard, select **Browse** to view your running application.

    The web app look like as shown in below figure
    
    ![webapp](images/webapp.png)

   The **Azure DevOps project** automatically configures a CI build and release trigger. You're now ready to collaborate with a team on a Java app with a CI/CD process that automatically deploys your latest work to your web site.


## Exercise 2: Commit the code changes and execute CI/CD

The Azure DevOps project created a **Git** repository in your VSTS account. Follow the steps below to view the repository and make code changes to your application.

1. Click on **Code** tab to view the created Git repository by Azure DevOps project.

     ![codetab](images/codetab.png)

1. You can clone your Git repository in your favorite IDE but for the purpose of this lab, you will edit in the browser itself and commit the changes directly to the master branch.

1. On the left-hand side of the browser, navigate to the **src/main/resources/static/index.html** file. Select **Edit**, and make a change. For example, change line number #21 to 
         
    ```csharp
         <div class="success-text">Welcome to Velocity 2018!!! </div>
    ```
1. Choose **Commit**, to save and check-in your changes.


1. In your browser, navigate to the **Builds**. You should now see a build is in progress. The changes you just made are automatically built and deployed via a VSTS CI/CD pipeline. Once the build is complete, navigate to **Releases** and and you will notice the deployment has started. 

1. Wait for the release to finish and then return to the Azure DevOps project on the Azure portal. On the right side of the dashboard, select **Browse** to view your updated running application. You will the updated description line in the web app.

   ![updatedsite](images/updatedsite.png)