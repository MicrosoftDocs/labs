
# Testing your PWA

This section is intended to test the PWA app we have created in the previous section in multiple platforms.

Prerequisites:

- This task has a dependency on task [Build a PWA with PWA Builder](Lab-part-3.md) and the prerequisites found there.


##	How to test on Windows 10

### Sideload a PWA

In case that you do not want to distribute your app through Microsoft Store, you can sideload the app package directly to a device.

1. Make sure your PC is in “dev” mode. Go to “settings > Update & Security > For developers” and then activate dev mode.

2. Go to the downlaoded windows 10 files (on the previous lab)

![Provide a URL](/Media/Picture36.jpg)

3. Open the “Windows 10” folder and right click on the powershell script called “test_install.ps1” and run it with powershell.
![Provide a URL](/Media/Picture37.jpg)

4. Open your start menu / start screen and look for app in “recently added” or search for the name from your manifest, launch the app and check that works as expected.

<img src="/Media/Picture38.PNG"><br>

### F12 tools for PWA

Prerequisites:
- Install the Microsoft Edge DevTools [download here.](https://www.microsoft.com/store/productId/9MZBFRMZ0MNJ)

The Microsoft Edge DevTools provide web developers with tools for attaching to open local and remote page targets and debug web content in web sites and apps on Windows.

1. Launch the app you have previously installed.

2. Launch the Edge Dev tools and select the app to attach to the process.

 <img src="/Media/Picture39.PNG"><br>

3. Verify that the service worker is up and running.

 <img src="/Media/Picture40.PNG"><br>

4. Verify that the app is caching content.

<img src="/Media/Picture41.PNG"><br>

### Run PWA [on Visual Studio >> ](Lab-part-8.md)

### App quality review

To be able to test the quality of our web app and get different metrics we will use a tool called `Sonarwhal`.

**What is sonarwhal?**
sonarwhal is a linting tool for the web, with a strong focus on the developer experience: easy to configure, develop, and well documented.

sonarwhal doesn’t want to reinvent the wheel. For that reason it tries to integrate other tools and services that do a great job, and contribute back where appropriate. For example, we are using aXe for accessibility, SSL Server Test for checking the certificate configuration, etc.

**What is sonarwhal’s goal?**
We have a few:

Bring the community together to decide what best practices are in several areas.
Help web developers write the best possible code.
Clean up the web of bad practices.
Promote community tools and services that do an awesome job but could not be known by everybody.


1. Go to the online version [Sonarwhal online](https://sonarwhal.com/)

<img src="/Media/Picture42.PNG"><br>

2. Introduce the url of the web app to be analyzed, hit the `RUN SCAN` button and wait for the report to be finished.

<img src="/Media/Picture43.PNG"><br>

3. Go to the PWA section and check out that the web app has no errors so that it is PWA compliant.

<img src="/Media/Picture44.PNG"><br>


### Pwa suggestions

On the PWA section, there is the possibility to see the details of the error/warning as well as a link to a page that shows the documentation about that error/warning and suggestions on how to solve it.

1. Hit the documentation button of the warning `The file extension should be 'webmanifest' (not 'json')` and check what might be the cause that triggered the warning and the suggestion to pass the rule.

<img src="/Media/Picture45.PNG"><br>

2. Navigate to the directory `Microsoft.Knowzy.WebApp\wwwroot\` in the source code and rename the manifest extension as `manifest.webmanifest`.

3. Navigate to the file located at `Microsoft.Knowzy.WebApp\Views\Shippings\Index.cshtml` in the source code and replace the manifest link with `<link rel="manifest" href="~/manifest.webmanifest">`

4. Publish your web app again in Azure [Deploy your ASP.net App Changes](Lab-part-2.md)

5. Scan again the web app using Sonarwhal and check that the webmanifest warning has been solved.

<img src="/Media/Picture46.PNG"><br>

6. Now let's consider that we created a manifest with some required parameters missing such as `name` and `short_name`. Modify the manifest located at `Microsoft.Knowzy.WebApp\wwwroot\` in the source code and remove the name and short_name parameters.

<img src="/Media/Picture47.PNG"><br>

7. Publish again the web app in Azure [Deploy your ASP.net App Changes](Lab-part-2.md)

8. Go to Sonarwhal and execute the scan again. Noticed now that there is a warning in the PWA section stating that the name is missing.

<img src="/Media/Picture48.PNG"><br>

9. Include again the `name` and `short_name` in the manifest. Publish in Azure the changes and execute Sonarwhal again. The warning should disappear.

### SRI suggestions

Apart from PWA suggestions, Sonarwhal offers other suggestions for performance, security, accessibility and interoperability.

In the security section, the Subresource Integrity `SRI` feature enables you to mitigate the risk of attacks, by ensuring that the files your Web app fetches, for instance from a CDN, have been delivered without a third-party having injected any additional content into those files.

1. On the Security section, go to the SRI section and click the Details button to check the cause of the security issues.

<img src="/Media/Picture49.PNG"><br>

2. As Sonarwhal suggested, in our web app is missing the integrity attribute in scripts and stylesheets. Go to the following link to generate the hash the each script and stylesheet [SRI Hash Generator](https://www.srihash.org/)

3. On the Hash generator site, introduce the URL of the site.min.css stylesheet `https://msftknowzy.azurewebsites.net/css/site.min.css` and copy the integrity hash in the stylesheet link in the _Layout.cshtml file located in the directory `Microsoft.Knowzy.WebApp\Views\Shared\` as shown below:

<img src="/Media/Picture50.PNG"><br>

```JS
#Microsoft.Knowzy.WebApp\Views\Shared\_Layout.cshtml
<link rel="stylesheet" href="~/css/site.min.css" asp-append-version="true" integrity="sha384-AaDfVej49Yw0nJPI8iAVmlJLD51g8CHg40usee6R5cY/DD5Jfss2VY05cPlz9lpf" />
```

4. Do the same previous steps for the site.min.js script `https://msftknowzy.azurewebsites.net/js/site.min.js` script.

5. Add the integrity hash for the bootstrap CDN stylesheet as shown below:

```JS
#Microsoft.Knowzy.WebApp\Views\Shared\_Layout.cshtml
<link rel="stylesheet" href="https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/css/bootstrap.min.css"
              asp-fallback-href="~/lib/bootstrap/dist/css/bootstrap.min.css"
              asp-fallback-test-class="sr-only" asp-fallback-test-property="position" asp-fallback-test-value="absolute" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
```
6. Launch again Sonarwhal scan and check that all the SRI security issues have been solved.


###	How to test on Chrome

1. Open up the our app using Chrome browser [PWA App](https://msftknowzy.azurewebsites.net/).

2. Open up the F12 tooling in the browser and select the `Application` tab.

3. Select the `Manifest` option and check that the manifest has been found and settings are correct.

<img src="/Media/Picture51.PNG"><br>

4. Select the `Service Workers` option and check that the service worker is up and running.

<img src="/Media/Picture52.PNG"><br>

5. Navigate to `Receiving` page and check that the path to `Receiving page` has been added to the Cache Storage.

<img src="/Media/Picture53.PNG"><br>

6. Check that the offline service worker feature is working fine by selecting the `Offline` checkbox and reloading the page. The `Receiving page` should be shown even though being offline.

<img src="/Media/Picture54.PNG"><br>

###	How to test on iOS

1. Open up the Web app using Safari browser [Web App](https://msftknowzy.azurewebsites.net/).

2. Select `Add to Home Screen` button in order to install the app on the device.

<img src="/Media/Picture55.PNG"><br>

3. Add a name that will be shown once installed on the device

<img src="/Media/Picture56.PNG"><br>

4. Verify that the app is installed.

<img src="/Media/Picture57.PNG"><br>

5. Launch the app and verify that works properly.

<img src="/Media/Picture58.PNG"><br>

### PWA on windows [next task >> ](Lab-part-7.md)
