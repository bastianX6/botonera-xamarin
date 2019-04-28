using System;
using botonera.Entities;
using botonera.Repository.FileDownload;
using botonera.Repository.HUD;
using botonera.Repository.PlaySongLocal;
using botonera.ViewModel;
using Xamarin.Forms;

namespace botonera.View.SongList
{
    public class LocalSongListActions: ISongListActions
    {
        SongListViewModel viewModel;
        FirebaseStorageFileManager fileManager;
        IAudioPlayer audioPlayer;
        IHud hud;


        public LocalSongListActions(SongListViewModel viewModel)
        {
            fileManager = new FirebaseStorageFileManager();
            fileManager.OnFileDownloaded += FileManager_OnFileDownloaded;
            audioPlayer = DependencyService.Get<IAudioPlayer>();
            hud = DependencyService.Get<IHud>();
            this.viewModel = viewModel;
        }

        public void ButtonClock_Clicked(object sender, EventArgs e)
        {
            hud.ShowError("Coming soon");
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
            hud.ShowInfo("Downloading file...");
            fileManager.DownloadFile(song.songName);
        }

        void FileManager_OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            hud.Dismiss();
            if(e.FileSaved)
            {
                PlayFile(e.FileName);
            }
            else
            {
                hud.ShowError("Couldn't download file");
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
