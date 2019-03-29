using System;
using System.Threading.Tasks;
using botonera.Entities;

namespace botonera.Repository
{
    public interface ISongListRepository
    {
        Task<SongResponseEntity> getSongs();
    }
}
