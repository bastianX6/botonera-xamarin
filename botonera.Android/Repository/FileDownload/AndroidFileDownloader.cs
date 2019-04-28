using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using botonera.Droid.Repository.FileDownload;
using botonera.Repository.FileDownload;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidFileDownloader))]
namespace botonera.Droid.Repository.FileDownload
{
    public class AndroidFileDownloader: IFileManager
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public void DownloadFile(string url, string folder, string fileName)
        {
            string pathToNewFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
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
            return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder, filename);
        }

        public bool DeleteAllFiles(string folder)
        {
            try
            {
                var path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
                Directory.Delete(path, true);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting songs folder: {ex}");
                return false;
            }
        }
    }
}
