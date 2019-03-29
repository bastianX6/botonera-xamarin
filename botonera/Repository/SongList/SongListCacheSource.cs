using System;
using System.Threading.Tasks;
using botonera.Entities;

namespace botonera.Repository.SongList
{
    public class SongListCacheSource: ISongListCache
    {
        public async Task<SongResponseEntity> GetSongList()
        {
            throw new NotImplementedException();
        }

        public void StoreSongs(SongResponseEntity songs)
        {
            throw new NotImplementedException();
        }
    }
}
