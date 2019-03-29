using System;
using System.Threading.Tasks;
using botonera.Entities;
using botonera.Repository.SongList;

namespace botonera.Repository
{
    public class SongListRepository: ISongListRepository
    {
        ISongListDataSource cloudSource;
        ISongListCache cacheSource;

        public SongListRepository(ISongListDataSource cloudSource, ISongListCache cacheSource)
        {
            this.cloudSource = cloudSource;
            this.cacheSource = cacheSource;
        }


        public SongListRepository() 
        {
            cloudSource = new SongListCloudSource();
            cacheSource = new SongListCacheSource();
        }

        public async Task<SongResponseEntity> GetSongs()
        {
            try
            {
                var songs = await cacheSource.GetSongList();
                return songs;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't get list from cache: " + ex);
                var remoteSongs = await cloudSource.GetSongList();
                storeSongsInCache(remoteSongs);
                return remoteSongs;
            }
        }


        private void storeSongsInCache(SongResponseEntity songs)
        {
            try
            {
                cacheSource.StoreSongs(songs);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't save song list in cache: " + ex);
            }
        }
    }
}
