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


        void SongList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var song = e.Item as SongEntity;
            System.Diagnostics.Debug.WriteLine("Song Code: {0} | Song Description: {1}", song.SongCode, song.Description);
        }

    }
}
