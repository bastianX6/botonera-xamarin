using System;
using System.Threading.Tasks;
using botonera.Entities;

namespace botonera.Repository.SongList
{
    public interface ISongListDataSource
    {
        Task<SongResponseEntity> GetSongList();
    }
}
