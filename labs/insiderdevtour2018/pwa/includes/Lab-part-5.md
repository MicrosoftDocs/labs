

# Publish your PWA


## Prerequisites

This task has a dependency on [step 1 Generate Manifest](lab-part-3.md) and [step 2 Build Service Worker](lab-part-4.md) to download the necessary code to transform your web application into PWA.

## Publish PWA
 Once you have done the previous step 1 and step 2 of PWABuilder tool, go to the next step 3 to download the generated code for your web app.

 The destination path of this files will be the scope of your project that you have define in the manifest before, in this case is the **wwwroot** because in our manifest we have specified the **./** root path.

> Remember: The scope member is a string that represents the navigation scope of this web application's application context.


+ Download the Web and Windows 10 code (you will use the Windows 10 downloaded files later).

![Provide a URL](/Media/Picture26.jpg)

### Add manifest to your site


1. Open the **WEB** downloaded files and go to the PWA folder inside projects (ex: \Downloads\Knowzy-web\projects\PWA).
![Provide a URL](/Media/Picture27.jpg)

2. Pull the "manifest.json" file and the "images" folder that are inside of the PWA folder that you obtained from the zip file in the previous step.
![Provide a URL](/Media/Picture28.jpg)

3. Add the manifest.json and the "images" folder to the **wwwroot** path of your site (ex: ...\source code\src\1. WebApp\Microsoft.Knowzy.WebApp\wwwroot).

    >Remember, if you change the path of your "images" folder, you need to update the json in your manifest file to reflect your changes.

    Using our Knowzy [ASP.NET](https://www.asp.net/) project the easiest way to add new content to a project is to drag and drop the contents from the file explorer into the solutions explorer of your project.
![Provide a URL](/Media/Picture29.jpg)

4. 4. Reference the manifest in your **/Views/Shared/_Layout.cshtml** page with a link tag:

    ````html
    <link rel="manifest" href="manifest.json">
    ````

![Provide a URL](/Media/Picture30.jpg)


  ### Add Service Worker code to your site

  Return to your downloaded PWA folder and go to projects (Downloads\Knowzy-web\projects\)

1. Copy **only** the pwabuilder-sw.js file from the "ServiceWorker1" folder to the wwwroot of your site.
![Provide a URL](/Media/Picture31.jpg)

![Provide a URL](/Media/Picture32.jpg)

2. Open up the landing page of your app (_Layout.cshtml) and add a new script tag in the head after the manifest link.
      ```html
        <link rel="manifest" href="manifest.json"></link>
        <script></script>
      ```

3. Add the following registration code inside the new script tag:

    ```js
    //This is the service worker with the Cache-first network

    //Add this below content to your HTML page, or add the js file to your page at the very top to register service worker
    if (navigator.serviceWorker.controller) {
      console.log('[PWA Builder] active service worker found, no need to register')
    } else {

    //Register the ServiceWorker
      navigator.serviceWorker.register('pwabuilder-sw.js', {
        scope: './'
      }).then(function(reg) {
        console.log('Service worker has been registered for scope:'+ reg.scope);
      });
    }
    ```

![Provide a URL](/Media/Picture33.jpg)


### Test locally
To test that your service worker is successfully installed launch the app in local make sure you are in the Miscrosoft.Knowzy.WebApp project and click on *IIS Express* button.
![](//Media/Picture7.jpg)

![](//Media/Picture34.jpg)

### Re-Publish Changes (optional)

Now that you have these powerful new features running locally, you can publish them to your website to be consumed as a PWA.

1. In Visual studio choose Project > Publish... or right click on Microsoft.Knowzy.WebApp and publish

    ![publish screen from vs](/Media/Picture13.jpg)

2. Choose "Microsoft Azure App Service" from the selection screen.

    > **NOTE** if your Visual Studio project is still debugging your Azure server or your local server, you may need to halt the server before re-publishing.

    ![publish screen from vs](/Media/Picture14.jpg)

3. Click on "Publish button", when it finishes will show automatically the application result.

    ![publish screen from vs](/Media/Picture1.jpg)

  ```Important!``` to show the download banner from other device, the website **must be acceded at least of two times.**
   *[Learn more about app install banners](https://developers.google.com/web/fundamentals/app-install-banners/).*
   In case of having problems you can do it manually by chrome settings, `add to home screen`:

  ![publish screen from vs](/Media/Picture35.jpg)



### Continue to [test your pwa >> ](lab-part-6.md)
