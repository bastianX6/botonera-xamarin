using System;
using System.Collections.Generic;
using Android.Media;
using botonera.Droid.Repository.PlaySongLocal;
using botonera.Repository.PlaySongLocal;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidAudioPlayer))]
namespace botonera.Droid.Repository.PlaySongLocal
{
    public class AndroidAudioPlayer: IAudioPlayer
    {
        List<MediaPlayer> mediaPlayerList;
        int maxCapacity = 4;

        public AndroidAudioPlayer()
        {
            mediaPlayerList = new List<MediaPlayer>(maxCapacity);
        }

        public void PlayClock()
        {
            throw new NotImplementedException();
        }

        public void PlaySong(string path)
        {
            if (mediaPlayerList.Count == maxCapacity) return;

            var player = new MediaPlayer();
            mediaPlayerList.Add(player);

            try
            {
                player.Error += Player_Error;
                player.Prepared += (s, e) =>
                {
                    player.Start();
                };
                player.Completion += Player_Completion;
                player.SetDataSource(path);
                player.PrepareAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception playing song: {ex}");
            }

        }

        public void StopAllSongs()
        {
            foreach (var mediaPlayer in mediaPlayerList)
            {
                mediaPlayer.Stop();
                mediaPlayer.Release();
            }
            mediaPlayerList.Clear();
        }

        void Player_Completion(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Player completed");
            var mediaPlayer = sender as MediaPlayer;
            mediaPlayer.Release();
            mediaPlayerList.Remove(mediaPlayer);
        }


        void Player_Error(object sender, MediaPlayer.ErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Player error: {e}");
        }

    }
}
