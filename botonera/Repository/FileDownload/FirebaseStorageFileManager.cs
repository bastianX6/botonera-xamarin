using System;
using Xamarin.Forms;

namespace botonera.Repository.FileDownload
{
    public class FirebaseStorageFileManager
    {
        private IFileManager fileManager;
        private string folderName = ".botonera_songs";

        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public FirebaseStorageFileManager()
        {
            fileManager = DependencyService.Get<IFileManager>();
            fileManager.OnFileDownloaded += Downloader_OnFileDownloaded;
        }

        public async void DownloadFile(string fileName)
        {
            var url = await FirebaseStorageHelper.SharedInstance.GetFile(fileName);
            fileManager.DownloadFile(url, folderName, fileName);
        }

        public bool FileExists(string filename)
        {
            return fileManager.FileExists(filename, folderName);
        }

        public string GetFilePath(string filename)
        {
            return fileManager.GetFilePath(filename, folderName);
        }

        void Downloader_OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (OnFileDownloaded != null)
                OnFileDownloaded.Invoke(this, e);
        }

    }
}
