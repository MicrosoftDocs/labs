
## PWA on Windows

Prerequisites: 

- This task has a dependency on task [Sideload a PWA](pwa_05_test.md).

###	Add some WinRT APIs (pwa builder)

PWA builder web site offers a collection of libraries for Windows APIs to add to your PWA application. 

To simplify the task, we will test the WinRT apis by using the F12 tools console.

#### Dark mode

1. Launch your PWA app from your start menu.

<img src="images/win_testing_app.PNG"><br>

2. Once initialized, click F12 tools and select the `console` tab.

3. Go to the preview pwa builder page and navigate to Windows API section [Windows API](https://preview.pwabuilder.com/windows).

4. Select `Create Dark Theme Check` and copy the source code.

<img src="images/winapi_darktheme_code.PNG"><br>

5. Copy the code in the console in F12 tools (step 2) and click Intro.

6. From the same console, call the function you previously entered. You should see whether your computer is set in dark or light theme.

<img src="images/winapi_darktheme_console.PNG"><br>

#### Timeline

1. Launch your PWA app from your start menu.

<img src="images/win_testing_app.PNG"><br>

2. Once initialized, click F12 tools and select the `console` tab.

3. Go to the preview pwa builder page and navigate to Windows API section [Windows API](https://preview.pwabuilder.com/windows).

4. Select `Add Activity to Windows Timeline check` and copy the source code.

<img src="images/winapi_timeline_code.PNG"><br>

5. Copy the code in the console in F12 tools (step 2) and click Intro.

6. From the same console, call the function you previously entered. Example:

```JS
addTimelineActivity("1", "KnowzyApp", "KnowzyApp demo app", "imagePath", "http://msftknowzy.azurewebsites.net");
```

<img src="images/winapi_timeline_console.PNG"><br>

7. Select our pwa app and put it in the foreground.

8. Go to the Windows Timeline functionality.

<img src="images/winapi_timeline_button.PNG"><br>

9. Verify that the new card we added has been added to Windows Timeline.

<img src="images/winapi_timeline_card.PNG"><br>