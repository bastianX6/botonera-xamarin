using System;
using botonera.Entities;

namespace botonera.Repository.SongList
{
    public interface ISongListCache: ISongListDataSource
    {
        void StoreSongs(SongResponseEntity songs);
    }
}
