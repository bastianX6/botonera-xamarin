using System;
using System.Net;
using System.Threading.Tasks;

namespace botonera.Repository.PlaySong
{
    public interface IPlaySongDataSource
    {
        Task<HttpStatusCode> PlaySong(string endpoint, string songCode);
        Task<HttpStatusCode> PlayClock(string endpoint);
        Task<HttpStatusCode> Stop(string endpoint);
    }
}
