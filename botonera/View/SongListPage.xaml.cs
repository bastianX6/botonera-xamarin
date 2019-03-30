using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using botonera.Entities;
using botonera.ViewModel;
using Xamarin.Forms;

namespace botonera.View
{
    public partial class SongListPage : ContentPage
    {
        SongListViewModel viewModel;

        public SongListPage()
        {
            InitializeComponent();
            viewModel = new SongListViewModel();
            ConfigureSongList();
        }

        private async void ConfigureSongList()
        {
            SongList.ItemsSource = viewModel.Songs;
            SongList.ItemTapped += SongList_ItemTapped;
            await viewModel.UpdateSongs();
        }

        async void SongList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var song = e.Item as SongEntity;
                var success = await viewModel.PlaySong(song.SongCode);
                System.Diagnostics.Debug.WriteLine($"Success: {success} Song Code: {song.SongCode} | Song Description: {song.Description}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on song play: {ex}");
            }

        }

        async void ButtonStop_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var success = await viewModel.Stop();
                System.Diagnostics.Debug.WriteLine($"Stop songs success: {success}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on song stop: {ex}");
            }
        }

        async void ButtonClock_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var success = await viewModel.PlayClock();
                System.Diagnostics.Debug.WriteLine($"Clock success: {success}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on clock song: {ex}");
            }
        }
    }
}
