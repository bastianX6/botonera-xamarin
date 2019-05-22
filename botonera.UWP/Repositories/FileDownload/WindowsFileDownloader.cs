using botonera.Repository.FileDownload;
using botonera.UWP.Repositories.FileDownload;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(WindowsFileDownloader))]
namespace botonera.UWP.Repositories.FileDownload
{
    class WindowsFileDownloader : IFileManager
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public bool DeleteAllFiles(string folder)
        {
            try
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder);
                Directory.Delete(path, true);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting songs folder: {ex}");
                return false;
            }
        }

        public void DownloadFile(string url, string folder, string fileName)
        {
            string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder);
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += Completed;
                webClient.Headers.Add("PrivateFileName", fileName);
                string pathToNewFile = Path.Combine(pathToNewFolder, fileName);
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on file download: {ex}");
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false, fileName));
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            var webClient = sender as WebClient;
            var fileName = webClient.Headers.Get("PrivateFileName");
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false, fileName));
            }
            else
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true, fileName));
            }
        }

        public bool FileExists(string filename, string folder)
        {
            string path = GetFilePath(filename, folder);
            return File.Exists(path);
        }

        public string GetFilePath(string filename, string folder)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder, filename);
        }
    }
}
