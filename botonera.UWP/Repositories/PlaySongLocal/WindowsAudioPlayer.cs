using botonera.Repository.PlaySongLocal;
using botonera.UWP.Repositories.PlaySongLocal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

[assembly: Dependency(typeof(WindowsAudioPlayer))]
namespace botonera.UWP.Repositories.PlaySongLocal
{
    class WindowsAudioPlayer : IAudioPlayer
    {
        List<MediaElement> mediaPlayerList;
        int maxCapacity = 4;

        public WindowsAudioPlayer()
        {
            mediaPlayerList = new List<MediaElement>(maxCapacity);
        }

        public void PlayClock()
        {
            throw new NotImplementedException();
        }

        public async void PlaySong(string path)
        {
            if (mediaPlayerList.Count == maxCapacity) return;

            try
            {
                var player = new MediaElement();
                mediaPlayerList.Add(player);
                var file = await StorageFile.GetFileFromPathAsync(path);
                player.SetPlaybackSource(MediaSource.CreateFromStorageFile(file));
                player.MediaEnded += Player_MediaEnded;
                player.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception playing song: {ex}");

            }
        }

        private void Player_MediaEnded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var player = sender as MediaElement;
            player.Stop();
        }

        public void StopAllSongs()
        {
            foreach (var mediaPlayer in mediaPlayerList)
            {
                mediaPlayer.Stop();
            }
            mediaPlayerList.Clear();
        }
    }
}
