using System;
namespace botonera.Repository.FileDownload
{
    public interface IFileManager
    {
        void DownloadFile(string url, string folder, string fileName);
        bool FileExists(string filename, string folder);
        string GetFilePath(string filename, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }

    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public string FileName = "";
        public DownloadEventArgs(bool fileSaved, string fileName)
        {
            FileSaved = fileSaved;
            FileName = fileName;
        }
    }
}
