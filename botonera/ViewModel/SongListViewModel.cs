using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using botonera.Entities;
using botonera.Repository;

namespace botonera.ViewModel
{
    public class SongListViewModel
    {
        ISongListRepository repository;
        public ObservableCollection<SongEntity> Songs;

        public SongListViewModel()
        {
            repository = new SongListRepository();
            Songs = new ObservableCollection<SongEntity>();
        }

        public SongListViewModel(ISongListRepository repository)
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



    }
}
