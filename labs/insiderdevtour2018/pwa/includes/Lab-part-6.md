
# Testing your PWA

This section is intended to test the PWA app we have created in the previous section in multiple platforms.

## Prerequisites:

- This task has a dependency on task [Build a PWA with PWA Builder](Lab-part-3.md) and the prerequisites found there.


##	How to test on Windows 10

### Sideload a PWA

In case that you do not want to distribute your app through Microsoft Store, you can sideload the app package directly to a device.

1. Make sure your PC is in “dev” mode. Go to “settings > Update & Security > For developers” and then activate dev mode.

2. Go to the downlaoded windows 10 files (on the previous lab)

3. Open the “Windows 10” folder and right click on the powershell script called “test_install.ps1” and run it with powershell.
![Provide a URL](/Media/Picture37.jpg)

4. Open your start menu / start screen and look for app in “recently added” or search for the name from your manifest, launch the app and check that works as expected.
<p align="center">
<img src="/Media/Picture71.jpg">
<img src="/Media/Picture38.PNG"><br>
</P>

### F12 tools for PWA

Prerequisites:
- Install the Microsoft Edge DevTools [download here.](https://www.microsoft.com/store/productId/9MZBFRMZ0MNJ)

The Microsoft Edge DevTools provide web developers with tools for attaching to open local and remote page targets and debug web content in web sites and apps on Windows.

1. Launch the app you have previously installed.

2. Launch the Edge Dev tools and select the app to attach to the process.

 <img src="/Media/Picture39.PNG"><br>

3. Go to the debugger tab and under the service worker section verify that the service worker is up and running.

 <img src="/Media/Picture40.PNG"><br>

4. Now under de Cache section, verify that the app is caching content.

<img src="/Media/Picture41.PNG"><br>

### Run PWA [on Visual Studio >> ](Lab-part-8.md)


###	How to test on Chrome

1. Open up the our app using Chrome browser [PWA App](https://msftknowzy.azurewebsites.net/).

2. Open up the F12 tooling in the browser and select the `Application` tab.

3. Select the `Manifest` option and check that the manifest has been found and settings are correct.

<img src="/Media/Picture51.PNG"><br>

4. Select the `Service Workers` option and check that the service worker is up and running.

<img src="/Media/Picture52.PNG"><br>

5. Navigate to `Receiving` page and check that the path to `Receiving page` has been added to the Cache Storage in the **Cache** section:

<img src="/Media/Picture53.PNG"><br>

6. Check that the offline service worker feature is working fine by going to the **service worker** section, select the `Offline` checkbox and reload the page. The `Receiving page` should be shown even though being offline.

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
