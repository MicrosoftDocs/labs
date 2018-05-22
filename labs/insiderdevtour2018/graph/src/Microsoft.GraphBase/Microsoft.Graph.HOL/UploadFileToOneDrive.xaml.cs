﻿// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.Utils;
    using System;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UploadFileToOneDrive : Page
    {
        public UploadFileToOneDrive()
        {
            this.InitializeComponent();
        }

        private async void Button_Upload_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var openPicker = new FileOpenPicker();
                openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                openPicker.FileTypeFilter.Add(".txt");
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");
                openPicker.FileTypeFilter.Add(".pdf");

                var file = await openPicker.PickSingleFileAsync();
                Progress.IsActive = true;
                uploadBtn.IsEnabled = false;
                await OneDriveHelper.UploadItem(file);
                InfoText.Text = "The files is your OneDrive in Hol/Graph/ folder";
            }
            catch (Exception ex)
            {
                InfoText.Text = $"OOPS! An error ocurred: {ex.GetMessage()}";
            }
            finally
            {
                Progress.IsActive = false;
                uploadBtn.IsEnabled = true;
            }
        }
    }
}
