
## PWA on Windows

Prerequisites:

- This task has a dependency on task **Sideload a PWA**.

###	Add some WinRT APIs (pwa builder)

PWA builder web site offers a collection of libraries for Windows APIs to add to your PWA application.

PWA Builder automatically add the code to allow WinRT access on the generated manifest.

<img src="../Media/Picture73.PNG">

We can see in the `uap:ApplicationContentUriRules` section the access allowed for our site.

<img src="../Media/Picture74.PNG">

To simplify the task, we will test the WinRT apis by using the F12 tools console.

#### Dark mode

1. Launch your PWA app from your start menu.

<img src="../Media/Picture59.PNG"><br>

2. Once initialized, click F12 button to open the F12 Developer tools and select the `console` tab.

3. [Go to the Windows API section of the PWA Builder Preview site](https://preview.pwabuilder.com/windows).

4. Select `Create Dark Theme Check` option and copy the source code in the **code** tab with the *copy* button.

<img src="../Media/Picture60.PNG"><br>

5. Paste the code in the F12 tools console and click Intro.

<img src="../Media/Picture72.PNG">

6. From the same console, call the function you previously entered. You should see whether your computer is set in dark or light theme.

<img src="../Media/Picture61.PNG"><br>

#### Timeline

To do this part your machine must have the last **Windows April Update** to be able to use Windows Timeline. Download the update from [software download](https://www.microsoft.com/en-us/software-download/windows10) page.

1. Launch your PWA app from your start menu.

<img src="../Media/Picture59.PNG"><br>

2. Once initialized, click F12 tools and select the `console` tab.

3. Go to the preview pwa builder page and navigate to [Windows API](https://preview.pwabuilder.com/windows) section.

4. Select `Add Activity to Windows Timeline check` option and copy the source code.

<img src="../Media/Picture62.PNG"><br>

5. Paste the code in the F12 console tools (step 2) and click Intro.

6. From the same console, call the function you previously entered. Example:

```JS
addTimelineActivity("1", "KnowzyApp", "KnowzyApp demo app", "imagePath", "https://msftknowzy.azurewebsites.net");
```

<img src="../Media/Picture63.PNG"><br>

7. For the app to be able to add the timeline tile, it's necessary for the app to be put in the foreground.

8. Go to the Windows Timeline functionality **Important!** Your machine need the Windows April update to continue with this step.
You can find the timeline icon on the taskbar or call out Win+Tab keyword shortcut, if you don't see it and have the update installed, restart your pc.

<img src="../Media/Picture64.png"><br>

9. Verify that the new card we added is shown in Windows Timeline.

<img src="../Media/Picture65.PNG"><br>

