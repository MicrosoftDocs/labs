Step 2: Adaptive Cards
======================

This step-by-step document will help you support Adaptive Cards into an existing
WPF application. [Adaptive Cards](https://adaptivecards.io/) are a new way for
developers to exchange card content in a common and consistent way --you can
play with samples on-line [here](https://adaptivecards.io/samples/).

From the UX perspective, using Adaptive Cards instead of common toast
notifications keeps our users in the experience provided in our app, instead of
moving their focus out of it. Also, it gives feedback to our users about
operations being done in a second plane, such as persisting data, or sending it
over the wire.

Adding support for Adaptive Cards to WPF
----------------------------------------

Microsoft provides multiple SDKs to extend Adaptive Cards support into multiple
technologies. From the WPF perspective, such is accessible through
[AdaptiveCards.Rendering.Wpf](https://www.nuget.org/packages/AdaptiveCards.Rendering.Wpf/)
NuGet package --by the date this guide was written the last available version is
1.0.0.

Make right-click into Microsoft.Knowzy.WPF project and choose Manage NuGet
Packages..., type “AdaptiveCards.Rendering.Wpf”. Select the mentioned package
and click Install.

![](/Media/Picture3.png)

Creating our card
-----------------

Since the app talks about list of items which correspond to tasks into a
production line, we’ll show a card once a new item is created, allowing even
later adding notes through it, as an option to fulfill information in case we
missed the chance previously.

From the app perspective, the card will be shown from MainView.xaml, so we’ll
begin by adding a container to render the card inside. Scroll down to
MainView.xaml bottom, and add a Grid to main one, placed at right-bottom:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [...]

        <Grid
            x:Name="adaptiveCardContainer"
            HorizontalAlignment="Right"
            VerticalAlignment="Bottom"
            Margin="10"/>

    </Grid>
</UserControl>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

adaptiveCardContainer is then accessed from code-behind to enlist our card as a
child.

Microsoft provides a few WYSIWYG options to design our cards: an [on-line
version](http://adaptivecards.io/visualizer/index.html?hostApp=Bot%20Framework%20WebChat)
and a [WPF-based
one](https://github.com/Microsoft/AdaptiveCards/tree/master/source/dotnet/Samples/WPFVisualizer),
among others. Such rely in JSON to specify the layout; however, we’ll write it
in C\# to easily reuse it when adding multiple items.

![](/Media/Picture4.png)

Open MainView.xaml.cs and add these namespaces:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using AdaptiveCards;
using AdaptiveCards.Rendering;
using AdaptiveCards.Rendering.Wpf;
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Next, add the following method, which returns an AdaptiveCard object to later
render it in above container --please note \_cardTitleTextBlock is declared as
class field, because we’ll update its text for reusal:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
private AdaptiveCard CreateCard()
{
    _cardTitleTextBlock = new AdaptiveTextBlock { Wrap = true };

    var card = new AdaptiveCard
    {
        Body =
        {
            new AdaptiveTextBlock("You added a new product")
            {
                Size = AdaptiveTextSize.Medium,
                Weight = AdaptiveTextWeight.Bolder
            },
            _cardTitleTextBlock
        },
        Actions =
        {
            new AdaptiveSubmitAction { Id = "Ok", Title = "OK" },
            new AdaptiveShowCardAction
            {
                Title = "Add some notes",
                Card = new AdaptiveCard
                {
                    Body =
                    {
                        new AdaptiveTextInput
                        {
                            Id = "Notes",
                            IsMultiline = true,
                            Placeholder = "Type here"
                        }
                    },
                    Actions = { new AdaptiveSubmitAction { Title = "Save" } }
                }
            }
        }
    };

    return card;
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

At ctor. level, we’ll the AdaptiveCardRenderer which will return us the WPF’s
FrameworkElement to add to container’s hierarchy. \_renderer and \_card are
readonly fields because we’ll reuse the same instances to render the different
cards:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public partial class MainView
{
    private readonly AdaptiveCardRenderer _renderer;
    private readonly AdaptiveCard _card;

    private AdaptiveTextBlock _cardTitleTextBlock;

    public MainView()
    {
        [...]

        InitializeComponent();

        var json = File.ReadAllText("Assets/WindowsNotificationHostConfig.json");
        var hostConfig = AdaptiveHostConfig.FromJson(json);
        _renderer = new AdaptiveCardRenderer(hostConfig);
        _card = CreateCard();
    }
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Adaptive Cards can be styled through JSON to make them fit the context in where
are being displayed, and here we want them to look like native Windows 10 toast
notifications. Add a new JSON file named WindowsNotificationHostConfig.json to
the Assets folder, with the following content --remember to mark its Copy to
Output Directory as Copy if newer within its Properties option at right-button
click menu:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
{
  "actions": {
    "actionAlignment": "stretch",
    "buttonSpacing": 4,
    "maxActions": 5,
    "showCard": {
      "inlineTopMargin": 12
    }
  },
  "adaptiveCard": {},
  "containerStyles": {
    "default": {
      "backgroundColor": "#FF1F1F1F",
      "foregroundColors": {
        "default": {
          "default": "#FFFFFFFF",
          "subtle": "#99FFFFFF"
        },
        "accent": {
          "default": "#FF419FFE",
          "subtle": "#99419FFE"
        },
        "dark": {
          "default": "#FF000000",
          "subtle": "#99000000"
        },
        "light": {
          "default": "#FFFFFFFF",
          "subtle": "#99FFFFFF"
        },
        "good": {
          "default": "#FF79AB3C",
          "subtle": "#9979AB3C"
        },
        "warning": {
          "default": "#FFFFF000",
          "subtle": "#99FFF000"
        },
        "attention": {
          "default": "#FFE81123",
          "subtle": "#99E81123"
        }
      }
    },
    "emphasis": {
      "backgroundColor": "#FF2E2E2E",
      "foregroundColors": {
        "default": {
          "default": "#FFFFFFFF",
          "subtle": "#99FFFFFF"
        },
        "accent": {
          "default": "#FF419FFE",
          "subtle": "#99419FFE"
        },
        "dark": {
          "default": "#FF000000",
          "subtle": "#99000000"
        },
        "light": {
          "default": "#FFFFFFFF",
          "subtle": "#99FFFFFF"
        },
        "good": {
          "default": "#FF79AB3C",
          "subtle": "#9979AB3C"
        },
        "warning": {
          "default": "#FFFFF000",
          "subtle": "#99FFF000"
        },
        "attention": {
          "default": "#FFE81123",
          "subtle": "#99E81123"
        }
      }
    }
  },
  "imageSizes": {
    "small": 32,
    "medium": 50,
    "large": 150
  },
  "imageSet": {
    "imageSize": "medium"
  },
  "factSet": {
    "title": {
      "weight": "bolder",
      "wrap": true,
      "maxWidth": 150
    },
    "value": {
      "wrap": true
    },
    "spacing": 10
  },
  "fontFamily": "Segoe UI",
  "fontSizes": {
    "small": 12,
    "default": 15,
    "medium": 20,
    "large": 24,
    "extraLarge": 34
  },
  "fontWeights": {
    "lighter": 400,
    "default": 500,
    "bolder": 700
  },
  "spacing": {
    "small": 8,
    "default": 15,
    "medium": 20,
    "large": 24,
    "extraLarge": 34,
    "padding": 12
  },
  "separator": {
    "lineThickness": 1,
    "lineColor": "#66FFFFFF"
  },
  "supportsInteractivity": true
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

This file was generated with [WPF
Visualizer](https://github.com/Microsoft/AdaptiveCards/tree/master/source/dotnet/Samples/WPFVisualizer)
tool, which also bundles different styles to play with, also affecting its
real-time rendering.

The following step will connect the item creation to actually displaying the
card.

Showing our card
----------------

Our app relies in MVVM for its architecture, so item creating is consistently
done at ViewModel level. At MainViewModel.cs, look for NewItem() method and add
a new property:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public void NewItem()
{
    var item = new ItemViewModel(_eventAggregator);
    _eventAggregator.PublishOnUIThread(new EditItemMessage(item));

    if (item.Id == null) return;
    DevelopmentItems.Add(item);

    // This prop. is just used to fire a visibility change in the UI, it should be improved in a real scenario
    ShowAdaptiveCard = true;
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Define ShowAdaptiveCard then to notify UI when it’s set:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
private bool _showAdaptiveCard;

public bool ShowAdaptiveCard
{
    get => _showAdaptiveCard;
    set
    {
        _showAdaptiveCard = value;
        NotifyOfPropertyChange(() => ShowAdaptiveCard);
    }
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Back to MainView.xaml/.xaml.cs, we first need to connect the new ViewModel prop.
to the View. At XAML level, add the following attribute to
adaptiveCardContainer:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [...]

        <Grid
            x:Name="adaptiveCardContainer"
            [...]
            Visibility="{Binding ShowAdaptiveCard, Converter={StaticResource BoolToVisibilityConverter}}"/>

    </Grid>
</UserControl>
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Now, when the prop. is true the Grid will be visible, and otherwise hidden.
Apart from this, we need another connection point to update the card with last
item’s data. Open the code-behind and within MainView_DataContextChanged() add
the following update:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
private void MainView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
{
    [...]
    viewModel.PropertyChanged += (_, args) =>
    {
        if (args.PropertyName == nameof(viewModel.ShowAdaptiveCard) && viewModel.ShowAdaptiveCard)
        {
            var lastItem = viewModel.DevelopmentItems.LastOrDefault();

            if (lastItem == null)
            {
                return;
            }

            UpdateAdaptiveCard(lastItem);
        }
    };
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Once we have MainViewModel set as DataContext, we subscribe to its
PropertyChanged event, in order to detect when ShowAdaptiveCard is true. When
it’s, we pick the last item added and update our card which will be immediately
shown due to the binding we added above.

Add UpdateAdaptiveCard() method to MainView.xaml.cs, which updates
\_cardTitleTextBlock, clears current container and adds a new rendering to it
again:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
private RenderedAdaptiveCard _renderedCard;

[...]

private void UpdateAdaptiveCard(ItemViewModel item)
{
    _cardTitleTextBlock.Text = $"{item.Name}, expected to start at " +
        $"{item.DevelopmentStartDate.ToShortDateString()} and completed at " +
        $"{item.ExpectedCompletionDate.ToShortDateString()}.";

    if (_renderedCard != null)
    {
        adaptiveCardContainer.Children.Clear();
        _renderedCard = null;
    }

    try
    {
        _renderedCard = _renderer.RenderCard(_card);
        adaptiveCardContainer.Children.Add(_renderedCard.FrameworkElement);
    }
    catch (AdaptiveException exception)
    {
        Debug.WriteLine(exception);
    }
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Build and run the project, and add a new item through top menu. As soon as you
save it, the Adaptive Card will pop up as expected:

![](/Media/Picture5.png)

Interacting with our card
-------------------------

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
private void UpdateAdaptiveCard(ItemViewModel item)
{
    _cardTitleTextBlock.Text = $"{item.Name}, expected to start at " +
        $"{item.DevelopmentStartDate.ToShortDateString()} and completed at " +
        $"{item.ExpectedCompletionDate.ToShortDateString()}.";

    if (_renderedCard != null)
    {
        adaptiveCardContainer.Children.Clear();
        _renderedCard.OnAction -= RenderedCard_OnAction;
        _renderedCard = null;
    }

    try
    {
        _renderedCard = _renderer.RenderCard(_card);
        _renderedCard.OnAction += RenderedCard_OnAction;
        adaptiveCardContainer.Children.Add(_renderedCard.FrameworkElement);
    }
    catch (AdaptiveException exception)
    {
        Debug.WriteLine(exception);
    }
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

You’ll notice there’s a new subscription done in the middle,
\_renderedCard.OnAction, which’s our entry-point to detecting when notes are
added within the card. Add RenderedCard_OnAction() just below:

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
private void RenderedCard_OnAction(RenderedAdaptiveCard sender, AdaptiveActionEventArgs e)
{
    if (e.Action is AdaptiveSubmitAction submitAction)
    {
        var viewModel = DataContext as MainViewModel;

        if (submitAction.Id == "Ok")
        {
            viewModel.ShowAdaptiveCard = false;
            return;
        }

        var inputs = sender.UserInputs.AsDictionary();
        var notes = inputs["Notes"];
        viewModel.UpdateNotes(notes);
        viewModel.ShowAdaptiveCard = false;

        sender.OnAction -= RenderedCard_OnAction;
    }
}
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Here we’re detecting when the user clicks on OK button to dismiss the card
--actually hide it; notice how we’re modifying the ViewModels’ prop. because the
binding will do the rest of the job-- and taking the notes to save them into the
item.

Summary
-------

After completing every step we’ve been able to show and interact with Adaptive
Cards from WPF to leverage the app’s value to our customers. We’ve learned how
to add support for Adaptive Cards in WPF, how to design new cards with WYSIWYG
tools, how to render such in WPF and later interact with its UI.
