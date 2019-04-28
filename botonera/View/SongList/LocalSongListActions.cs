using System;
using System.IO;
using System.Threading.Tasks;
using botonera.Entities;
using botonera.Repository.FileDownload;
using botonera.Repository.PlaySongLocal;
using botonera.ViewModel;
using Xamarin.Forms;

namespace botonera.View.SongList
{
    public class LocalSongListActions: ISongListActions
    {
        FirebaseStorageFileManager fileManager;
        IAudioPlayer audioPlayer;
        SongListViewModel viewModel;

        public LocalSongListActions(SongListViewModel viewModel)
        {
            fileManager = new FirebaseStorageFileManager();
            fileManager.OnFileDownloaded += FileManager_OnFileDownloaded;
            audioPlayer = DependencyService.Get<IAudioPlayer>();
            this.viewModel = viewModel;
        }

        public void ButtonClock_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ButtonRandom_Clicked(object sender, EventArgs e)
        {
            var random = new Random();
            var index = random.Next(0, viewModel.Songs.Count - 1);
            var song = viewModel.Songs[index];
            TryPlaySong(song);
        }

        public void ButtonStop_Clicked(object sender, EventArgs e)
        {
            audioPlayer.StopAllSongs();
        }

        public void SongList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var song = e.Item as SongEntity;
            TryPlaySong(song);
        }

        private void TryPlaySong(SongEntity song)
        {
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
            audioPlayer.PlaySong(songPath);

        }

    }
}
