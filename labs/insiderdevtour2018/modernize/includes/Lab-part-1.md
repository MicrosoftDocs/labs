Step 1: UI modernization
========================

The following guide will take you through the built-in WPF’s WebBrowser control,
which allows to embed web sites within our apps, to the modern WebView one,
relying in Edge’s engine to render cutting edge HTML5 and CSS3 standards, plus
all new features coming along, such like WebRTC, Service Workers, etc.

WPF’s built-in WebBrowser control
---------------------------------

Right click on Views folder, under Microsoft.Knowzy.WPF project, and select Add,
Window... Name it DocumentationView.xaml. Open such file and add the following
child control:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
<WebBrowser
        x:Name="webBrowser" />
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Now open its code-behind file, and add the following subscription --remember to
unsubscribe it under a real scenario:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public DocumentationView()
{
    InitializeComponent();

    DataContextChanged += DocumentationView_DataContextChanged;
}

private void DocumentationView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
{
    if (e.NewValue != null && e.NewValue is DocumentationViewModel viewModel)
    {
        webBrowser.Navigate(viewModel.URL);
    }
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

As you’ve noticed, we need DocumentationViewModel class: right-click on
ViewModels folder, Add, Class..., and name it DocumentationViewModel.cs. Such
will only provide the destiny URL so can bind it later on:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public class DocumentationViewModel : Screen
{
    private const string _url = "https://www.youtube.com/microsoft";

    public string URL => _url;
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

At AppBootstrapper.cs add the new ViewModel so can be injected along with the
View:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
protected override void Configure()
{
    var builder = new ContainerBuilder();
    [...]
    builder.RegisterType<DocumentationViewModel>().SingleInstance();
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Next, we’ll add a new button at top menu bar, so open MainView.xaml and the
following MenuItem at the end:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                <MenuItem Header="{x:Static localization:Resources.Help_Menu}" Template="{DynamicResource MenuItemControlTemplate}"
                          cal:Message.Attach="About()" />
                <MenuItem Header="Doc." Template="{DynamicResource MenuItemControlTemplate}"
                          cal:Message.Attach="Documentation()" />
            </Menu>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Our ViewModel needs to handle such attachment so go back to MainViewModel.cs and
add the following method:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public void Documentation()
{
    _eventAggregator.PublishOnUIThread(new OpenDocumentationMessage());
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

OpenDocumentationMessage is a new class which needs to be created, right at
Messages folder: follow the same steps as before and leave it as it’s, without
any logic inside.

Finally, open ShellViewModel.cs and add a new interface to its inheritance,
followed by a new readonly field which stores the DocumentationViewModel
instance, and its corresponding Handle() method, which opens the window as a
dialog:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public class ShellViewModel : [...], IHandle<OpenDocumentationMessage>
    {
        [...]
        private readonly DocumentationViewModel _documentationViewModel;

        public ShellViewModel([...], DocumentationViewModel documentationViewModel)
        {
            [...]
            _documentationViewModel = documentationViewModel;
        }

        [...]

        public void Handle(OpenDocumentationMessage message)
        {
            _windowManager.ShowDialog(_documentationViewModel);
        }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Build and run the solution to test above code, by clicking in Documentation
menu, right at top bar. It simply opens the DocumentationView’s window and
renders the URL specified within DocumentationViewModel.

![](../Media/Picture1.png)

As you can appreciate the rendered web site differs from such accessed through
more modern browsers such like Edge or Chrome.

Replacing WebBrowser with WebView
---------------------------------

In order to make WebView available from WPF, the package
[Microsoft.Toolkit.Win32.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.Controls/)
needs to be added to Microsoft.Knowzy.WPF project. Right click on this and left
click again on Manage NuGet Packages...

Choose Browse tab and type “Microsoft.Toolkit.Win32.UI.Controls“ at Search entry
--this package is in pre-release by the date of writing this guide, so check
Include prerelease option to assure it’s visible to us. Also, since it depends
on .NET Framework 4.6.2, the project must be re-targeted to this version which
may require installing the corresponding SDK (just follow Visual Studio
indications to achieve it).

![](../Media/Picture2.png)

Open Views/DocumentationView.xaml and replace WebBrowser tag with WebView, by
adding the following namespace at root Window:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
<Window x:Class="Microsoft.Knowzy.WPF.Views.DocumentationView"
        [...]
        xmlns:WPF="clr-namespace:Microsoft.Toolkit.Win32.UI.Controls.WPF;assembly=Microsoft.Toolkit.Win32.UI.Controls"
        mc:Ignorable="d"
        Title="DocumentationView" Height="450" Width="800">
    <WPF:WebView
        x:Name="webBrowser" />
</Window>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

There are no more changes to do, this’ all. Simply build and run the project and
you’ll enjoy the latest Edge-powered engine with noticeable changes in how
things are rendered.

Summary
-------

This guide has introduced WPF’s WebBrowser and showcased how web sites are
rendered using it. Following it we’ve moved to WebView by simply adding a NuGet
package and renaming the XAML tag, leveraging the latest state-of-art web
technologies.
