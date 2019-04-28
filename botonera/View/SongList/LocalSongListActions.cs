using System;
using System.Threading.Tasks;
using botonera.Entities;
using botonera.Repository.FileDownload;
using Xamarin.Forms;

namespace botonera.View.SongList
{
    public class LocalSongListActions: ISongListActions
    {
        FirebaseStorageFileManager fileManager;

        public LocalSongListActions()
        {
            fileManager = new FirebaseStorageFileManager();
            fileManager.OnFileDownloaded += FileManager_OnFileDownloaded;
        }

        public void ButtonClock_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ButtonRandom_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ButtonStop_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SongList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var song = e.Item as SongEntity;
            if (!fileManager.FileExists(song.songName))
            {
                // Download file and play after
                DownloadFile(song);
            }
            else
            {
                // play file
                PlayFile(song.songName);
            }
        }

        private void DownloadFile(SongEntity song)
        {
            fileManager.DownloadFile(song.songName);
        }

        void FileManager_OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if(e.FileSaved)
            {
                PlayFile(e.FileName);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Couldn't download file: {e.FileName}");
            }
        }


        private void PlayFile(string songName)
        {
            var songPath = fileManager.GetFilePath(songName);
            System.Diagnostics.Debug.WriteLine($"Trying to play {songPath}");
        }

    }
}
