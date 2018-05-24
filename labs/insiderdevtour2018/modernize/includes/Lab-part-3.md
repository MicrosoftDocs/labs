Step 3: Distribution and versioning
===================================

As colophon to this laboratory we’ll learn how our WPF app can be packaged for
distribution, including through Windows Store. Currently, the [open-sourced MSIX
platform](https://github.com/Microsoft/msix-packaging) has its tooling in
process, so we’ll stick with an intermediary option for packaging our app with
App Installer.

Packaging our WPF app for side-loading
--------------------------------------

Open Microsoft.Knowzy.WPF.sln, make right-click at its root within the Solution
Explorer and click Add, New Project... Choose Windows Application Packaging
(Visual C\#) template, located at Visual C\#, Windows Universal. This new
project will generate packages for us which can be both uploaded to the Store or
side-loaded.

![](../media/Picture6.png)

At such project, in the Solution Explorer, make right-click at Applications, Add
Reference... Check Microsoft.Knowzy.WPF and click OK. Now our package will
automatically contain the WPF app, and it’s all we have to do for now. As you
can appreciate, generating an installation’s really easy.

You can customize the app name, its icons and a few more options at
Package.appxmanifest file, but we’ll stick with predefined values for this lab.

Finally, in order to generate the package, make right-click at the project root
and choose Store, Create App Packages... This will open a new wizard which will
guide you through the process:

1.  Firstly, choose I want to create packages for sideloading, Next --uncheck
    Enable automatic updates because we’ll dig into this later

2.  We can customize the Output location, Version and architectures, among other
    things, but will leave them by default; Create --versions will auto-increase
    when creating new packages, so we don’t have to take care of this

![](../media/Picture7.png)

Once the process ends, a new dialog will link us to the path where the package
was left, accompanied by the chance to pass the Windows App Certification Kit,
if we’d like to go public through the Store, which’s not our case, so click
Close.

Right now you simply can double-click on the .appxbundle file and the new set-up
process will start, installing your app locally and adding it to the Start menu,
as if it was done through the Store --you even can uninstall it in the same way.

Enabling automatic updates
--------------------------

As you remember, we unchecked this option above within the packaging wizard, so
reopen again such and check it now. As a difference with previous one, this time
a new step’s shown asking where the updates will live, letting us choose between
a network resource path or a web URL. For the sake of simplification we’ll
choose a network path.

In order to serve a local path to the local network first create a new empty
folder at Desktop. Right-click on it, Properties, Sharing tab, Advanced
Sharing... Just check Share this folder and click OK. If you open a new Windows
Explorer and type \\\\localhost\\ at the address bar, you’ll notice your shared
folder’s now available --right now we just have read access through the share,
but can write on it accessing through the local folder.

Back to the wizard, paste above folder path into Installation URL (i.e.
“\\\\localhost\\Packages”) and leave Check everytime the application runs
selected --this way the app will check for updates at the specified URL upon
start, managing the updating process for us. Finally click on Create and wait
for the process to end.

At the summary window which will appear in a few seconds, click on Output
location and copy every file and folder contained into the served folder
--remember it was placed at Desktop\\.

If you double-click at index.html you’ll appreciate a similar experience to the
one Windows Store serves. Clicking on Get the app will launch the set-up as
before, but now the app will look for updates on each start.

![](../media/Picture8.png)

If you make any change to your apps, generate its package and copy such back to
the shared folder, the installed one will know a new update’s available when it
starts, and will kindly ask you to update it-self.

Summary
-------

This guide’s taken us into packaging a common WPF app with the new App Installer
platform, leveraging automatic updates handling and providing a seamless User
Experience. Within the following months the new MSIX platform tooling will be
available, simplifying even further the package creation.
