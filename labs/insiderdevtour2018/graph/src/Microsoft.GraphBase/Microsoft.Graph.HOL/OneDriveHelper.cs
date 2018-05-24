namespace Microsoft.Graph.HOL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Microsoft.Graph.HOL.Utils;

    public class OneDriveHelper
    {
        public static async Task<List<DriveItem>> GetRecentItems()
        {
            //throw new NotImplementedException();            

            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                var onedrive = await graphClient.Me.Drive.Recent().Request().GetAsync();
                return onedrive.Take(10).ToList();
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error get recent files in OneDrive: " + ex.Message);
                throw;
            }
        }

        public static async Task UploadItem(StorageFile storageFile)
        {
            throw new NotImplementedException();

        }

        public static async Task<Stream> DownloadFile(DriveItem driveItem)
        {
            throw new NotImplementedException();           
        }

        public static async Task<Stream> ConvertContetPDF(DriveItem driveItem)
        {
            throw new NotImplementedException();            
        }

        private static bool ValidateExtension(string filename)
        {
            string extension = "doc, docx, epub, eml, htm, html, md, msg, odp, ods, odt, pps, ppsx, ppt, pptx, rtf, tif, tiff, xls, xlsm, xlsx";
            return extension.Contains(filename.GetExtension());
        }

        public static async Task<List<DriveItem>> GetItems(int numberOfElements)
        {
            throw new NotImplementedException();          
        }

        private static async Task<List<DriveItem>> GetNameFiles(GraphServiceClient graphClient, List<DriveItem> filesName, IDriveItemChildrenCollectionPage items, int numberOfElements)
        {

            foreach (var item in items)
            {
                if (item.File != null)
                {
                    filesName.Add(item);
                }
                else
                {
                    var driveItemInfo = graphClient.Me.Drive.Items[item.Id].Children.Request().GetAsync().Result;
                    await GetNameFiles(graphClient, filesName, driveItemInfo, numberOfElements);
                }

                if (filesName.Count == numberOfElements)
                {
                    break;
                }

            }

            return filesName;
        }
    }
}
