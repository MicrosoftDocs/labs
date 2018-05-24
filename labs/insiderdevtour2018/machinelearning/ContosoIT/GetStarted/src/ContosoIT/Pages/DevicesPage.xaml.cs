﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using ContosoIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Visibility = Windows.UI.Xaml.Visibility;

namespace ContosoIT.Pages
{
    public sealed partial class DevicesPage : Page
    {
        public DevicesPage()
        {
            InitializeComponent();
        }

        public List<string> Suggestions { get; set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var selectedFile = (DetectionDataParametersModel)e.Parameter;
            await BeginDetection(selectedFile);
        }

        private async Task BeginDetection(DetectionDataParametersModel detectionDataParameters)
        {
            if (detectionDataParameters == null)
            {
                return;
            }

            UpdateProgressRingAndResultsVisibility(true);

            // Your code goes here

            var classLabel = "surface-pro";
            await ShowResults(detectionDataParameters.SelectedFile, classLabel);
        }

        // Show ProgressRing and hide results grid
        // true -> shows progress ring and hides results grid
        // false -> hides progress ring and shows results grid
        private void UpdateProgressRingAndResultsVisibility(bool show)
        {
            Progress.IsActive = show;
            MainGrid.Visibility = show ? Visibility.Collapsed : Visibility.Visible;
            NoDeviceFound.Visibility = Visibility.Collapsed;
        }

        private async Task ShowResults(IStorageFile file, string label)
        {
            DetectedLabel.Text = label == DetectionConstants.SurfaceStudioTag ? 
                DetectionConstants.SurfaceStudio : DetectionConstants.SurfacePro;
            Suggestions = label == DetectionConstants.SurfaceStudioTag ?
                DetectionConstants.SurfaceStudioSuggestions : DetectionConstants.SurfaceProSuggestions;
            SuggestionsListView.ItemsSource = Suggestions;

            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(stream);
                DetectedImage.Source = bitmapImage;
            }

            UpdateProgressRingAndResultsVisibility(false);
        }

        private static async Task<VideoFrame> ConvertFileToVideoFrameAsync(IStorageFile file)
        {
            SoftwareBitmap softwareBitmap;
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var decoder = await BitmapDecoder.CreateAsync(stream);
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore);
            }

            return VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);
        }
    }
}