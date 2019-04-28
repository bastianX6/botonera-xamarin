using System;
namespace botonera.Repository.PlaySongLocal
{
    public interface IAudioPlayer
    {
        void PlaySong(string path);
        void PlayClock();
        void StopAllSongs();
    }
}
