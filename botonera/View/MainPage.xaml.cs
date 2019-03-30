using System;
using System.Collections.Generic;
using botonera.Entities;
using botonera.ViewModel;
using Xamarin.Forms;

namespace botonera.View
{
    public partial class MainPage : ContentPage
    {
        SongListViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new SongListViewModel();
            configureSongList();
        }

        private void configureSongList()
        {
            SongList.ItemsSource = viewModel.Songs;
            SongList.ItemTapped += SongList_ItemTapped;
            viewModel.UpdateSongs();
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
                System.Diagnostics.Debug.WriteLine($"Error on song play {ex}");
            }

        }

    }
}
