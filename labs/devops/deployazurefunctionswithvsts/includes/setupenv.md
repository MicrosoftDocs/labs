### Part A: Verify the created Azure resources

 In this lab, you will be using a fictional eCommerce website - PartsUnlimited. The PartsUnlimited team wants to  roll out a new discount for its employees and customers and wants to build Azure Functions that will retrieve the right discount depending on whether the logged in user is an employee or a customer. 

Let us verify the resources created in the Azure Portal. 

1. Open your browser and navigate to [https://portal.azure.com](https://portal.azure.com)

1. Login with the following username and password:
   > Username: ++@lab.CloudPortalCredential(1).Username++    
   > Password: ++@lab.CloudPortalCredential(1).Password++


1. Navigate to the **Resource Groups** and select **@lab.CloudResourceGroup(268).Name** to view the resources. You should see 3 resources as shown below.

   ![azure_resources](../images/azure_resources.png)

### Part B: Create Visual Studio Team Services account

Next, you will provision a Team services account.

1. Navigate to https://www.visualstudio.com/team-services/ in a separate tab. Select **Get Started for Free**.

1. You can use the same credentials used above to log in to Azure
     > Username: ++@lab.CloudPortalCredential(1).Username++      
     > Password: ++@lab.CloudPortalCredential(1).Password++

1. Provide a name for your Visual Studio Team Services account and click **Continue** to start the creation process.

1. In a few minutes, your account should be ready with a default project **MyFirstProject** created.

### Part C: Generate project data with VSTS Demo Generator

1. Use the [VSTS Demo Generator](https://demogentesting.azurewebsites.net/?TemplateId=77376&Name=AzureFunctions_BuildWorkshop) to provision the project on your VSTS account.

   > VSTS Demo Generator helps you create team projects on your VSTS account with sample content that include source code, work items,iterations, service endpoints, build and release definitions based on the template you choose during the configuration.

   ![vsts demo generator](../images/vstsdemogeneratornew.png)

1. Click the **Sign In** button to get started. If you are asked for credentials, sign in with the same credentials used above to log in to Azure
     > Username: ++@lab.CloudPortalCredential(1).Username++      
     > Password: ++@lab.CloudPortalCredential(1).Password++  

1. Accept the request for permissions by clicking on the **Accept** button. 

   ![accept terms](../images/acceptterms.png)
     
1. Select the previously created Team Services account from the drop down, provide the project name as **PartsUnlimited** and click Create Project.

    ![create project](../images/createproject.png)

1. Once the project is created, click on the generated URL to be directed to the project portal in a new tab.

    ![create project](../images/createdproject.png)

1. Navigate to the **Code** hub within the project portal, select **Clone** and then select **Clone in Visual Studio**. 

   ![cloneinvisualstudio](../images/cloneinvisualstudio.png)

   Note that VSTS supports a wide variety of IDEs including Eclipse, IntelliJ, XCode, Android Developer Studio, Visual Studio Code, etc.

1. When the code opens in Visual Studio, if you are prompted to sign into Visual Studio Team Services, use the same credentials(that you used above to create the VSTS account) and select **Clone**

1. You can use the same credentials used above to log in to Azure
     > Username: ++@lab.CloudPortalCredential(1).Username++      
     > Password: ++@lab.CloudPortalCredential(1).Password++

     ![clonepath](../images/clonepath.png)


1. Once it is cloned, you should see **PartsUnlimited.sln** under **Solutions** in the Team Explorer.
     ![openproject](../images/openproject.png)
