using System;
using System.Threading.Tasks;
using botonera.Entities;
using botonera.Repository.PlaySong;
using botonera.Repository.SongList;

namespace botonera.Repository
{
    public class SongsRepository: ISongsRepository
    {
        ISongListDataSource cloudSource;
        ISongListCache cacheSource;
        IPlaySongDataSource playSongCloudSource;

        public SongsRepository(ISongListDataSource cloudSource, ISongListCache cacheSource, IPlaySongDataSource playSongCloudSource)
        {
            this.cloudSource = cloudSource;
            this.cacheSource = cacheSource;
            this.playSongCloudSource = playSongCloudSource;
        }


        public SongsRepository()
        {
            cloudSource = new SongListCloudSource();
            cacheSource = new SongListCacheSource();
            playSongCloudSource = new PlaySongCloudSource();
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

        public async Task<bool> PlaySong(string endpoint, string songCode)
        {
            var statusCode = await playSongCloudSource.PlaySong(endpoint, songCode);
            return statusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> PlayClock(string endpoint)
        {
            var statusCode = await playSongCloudSource.PlayClock(endpoint);
            return statusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> Stop(string endpoint)
        {
            var statusCode = await playSongCloudSource.Stop(endpoint);
            return statusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
