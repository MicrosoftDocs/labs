### PWA on Windows

Prerequisites:

- This task has a dependency on **Sideload a PWA** section.

####	Add some WinRT APIs (pwa builder)

PWA builder web site offers a collection of libraries for Windows APIs to add to your PWA application.

PWA Builder automatically adds the code to allow WinRT access on the generated manifest.

<img src="../pwa/media/Picture73.png">

We can see in the `uap:ApplicationContentUriRules` section the access allowed for our site.

<img src="../pwa/media/Picture74.png">

To simplify the task, we will test the WinRT APIs by using the F12 tools console.

##### Dark mode

1. Launch your PWA app from your start menu.

<img src="../pwa/media/Picture59.png"><br>

2. Once initialized, click F12 button to open the F12 Developer tools and select the `console` tab.

3. [Go to the Windows API section of the PWA Builder Preview site](https://preview.pwabuilder.com/windows).

4. Select `Create Dark Theme Check` option and copy the source code in the **code** tab with the *copy* button.

<img src="../pwa/media/Picture60.png"><br>

5. Paste the code in the F12 tools console and hit enter.

<img src="../pwa/media/Picture72.png">

6. From the same console, call the function you previously entered. You should see whether your computer is set in dark or light theme.

<img src="../pwa/media/Picture61.png"><br>

##### Timeline

To do this part, your machine must have the last **Windows April Update** to be able to use Windows Timeline. Download the update from [software download](https://www.microsoft.com/software-download/windows10) page.

1. Launch your PWA app from your start menu.

<img src="../pwa/media/Picture59.png"><br>

2. Once initialized, click F12 tools and select the `console` tab.

3. Go to the preview PWA builder page and navigate to [Windows API](https://preview.pwabuilder.com/windows) section.

4. Select `Add Activity to Windows Timeline check` option and copy the source code.

<img src="../pwa/media/Picture62.png"><br>

5. Paste the code in the F12 console tools (step 2) and hit Enter.

6. From the same console, call the function you previously entered.

```JS
addTimelineActivity("1", "KnowzyApp", "KnowzyApp demo app", "imagePath", "https://msftknowzy.azurewebsites.net");
```

<img src="../pwa/media/Picture63.png"><br>

7. To add the app to the timeline tile, it's necessary for the app to be in the foreground.

8. Go to the Windows Timeline functionality (**Important!** Your machine need the Windows April update to continue with this step.
You can find the timeline icon on the taskbar or call out Win+Tab keyword shortcut, if you don't see it and have the update installed, restart your pc).

<img src="../pwa/media/Picture64.png"><br>

9. Verify that the new card we added is shown in Windows Timeline.

<img src="../pwa/media/Picture65.png"><br>
