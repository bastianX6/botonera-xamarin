using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using botonera.Entities;
using botonera.Repository;
using botonera.Utils;

namespace botonera.ViewModel
{
    public class SongListViewModel
    {
        ISongsRepository repository;
        public ObservableCollection<SongEntity> Songs;
        private string endpoint => PropertiesManager.Endpoint;

        public SongListViewModel()
        {
            repository = new SongsRepository();
            Songs = new ObservableCollection<SongEntity>();
        }

        public SongListViewModel(ISongsRepository repository)
        {
            this.repository = repository;
            Songs = new ObservableCollection<SongEntity>();
        }

        public async Task UpdateSongs()
        {
            var songResponse = await repository.GetSongs();
            foreach (var songEntity in songResponse.Songs)
            {
                Songs.Add(songEntity);
            }
        }

        public async Task<bool> PlaySong(string songCode)
        {
            return await repository.PlaySong(endpoint, songCode);
        }

        public async Task<bool> PlayClock()
        {
            return await repository.PlayClock(endpoint);
        }

        public async Task<bool> Stop()
        {
            return await repository.Stop(endpoint);
        }
    }
}
