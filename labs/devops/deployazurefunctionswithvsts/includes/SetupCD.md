

1. Once the build succeeds, click the **Release** option from the **Build & Release** hub.

    ![Release Hub](images/releasehub.png)

1. In the **Release** page, select the definition **AzureFunctions_CD** and click **Edit**.

     ![edit release def](images/editreleasedef.png)

1. Select the artifact trigger and make sure the **Continuous deployment trigger** is enabled.

     ![cdtrigger](images/cdtrigger.png)

1. To deploy **PartsUnlimited Website**, click **Tasks**, select the first **Deploy PartsUnlimited Website** task and configure the inputs as shown below.

    ![websitedeploytask](images/websitedeploytask.png)

   > To authorize the **Azure Subscription**, first select the **Azure subscription** from the drop down and then the drop down within the **Authorize** button. Click the drop down, choose **Advanced Options** and authorize Team Services to connect to the Azure subscription.

    ![websitedeploytask](images/authorizeazure.png)

    ![websitedeploytask](images/azureauth.png)

1. For the **PartsUnlimited APIs**, select the second task and configure the inputs as shown below.

   ![apideploytask](images/apideploytask.png)

1. Select the third task to deploy **PartsUnlimited Azure Function** and configure the inputs  as shown below.

   ![functionappdeploy](images/functionappdeploy.png)
  
1. Click **Save**. In the Save dialog box, click **OK**. To test the release definition, click **Release** and then **Create Release**.
  
   ![createrelease](images/createrelease.png)

   On the Create new release dialog box, click **Create**.

1. You will notice a new release being created. Select the link to navigate to the release.

   ![releasetriggered](images/releasetriggered.png)
   
   You can watch the live logs for the deployment as it happens. Wait for the release to be deployed to the Azure web app.

      ![releaselogs](images/releaselogs.png)

   Wait for the release to complete and succeed before proceeding to the next section.