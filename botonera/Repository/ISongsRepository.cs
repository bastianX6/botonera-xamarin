using System;
using System.Threading.Tasks;
using botonera.Entities;

namespace botonera.Repository
{
    public interface ISongsRepository
    {
        Task<SongResponseEntity> GetSongs();
        Task<bool> PlaySong(string endpoint, string songCode);
        Task<bool> PlayClock(string endpoint);
        Task<bool> Stop(string endpoint);

    }
}
