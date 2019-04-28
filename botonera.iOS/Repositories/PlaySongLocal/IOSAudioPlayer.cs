using System;
using System.Collections.Generic;
using AVFoundation;
using botonera.iOS.Repositories.PlaySongLocal;
using botonera.Repository.PlaySongLocal;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSAudioPlayer))]
namespace botonera.iOS.Repositories.PlaySongLocal
{
    public class IOSAudioPlayer : IAudioPlayer
    {
        List<AVAudioPlayer> mediaPlayerList;
        int maxCapacity = 4;

        public IOSAudioPlayer()
        {
            mediaPlayerList = new List<AVAudioPlayer>(maxCapacity);
        }

        public void PlayClock()
        {
            throw new NotImplementedException();
        }

        public void PlaySong(string path)
        {
            if (mediaPlayerList.Count == maxCapacity) return;

            try
            {
                var url = NSUrl.FromString(path);
                var player = AVAudioPlayer.FromUrl(url);
                mediaPlayerList.Add(player);
                player.FinishedPlaying += Player_FinishedPlaying;
                player.Play();
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
            }
            mediaPlayerList.Clear();
        }


        void Player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Player completed");
            var player = sender as AVAudioPlayer;
            mediaPlayerList.Remove(player);
            player = null;
        }

    }
}
