using System;
using System.Collections.Generic;
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
            SongList.ItemsSource = viewModel.Songs;
            viewModel.UpdateSongs();
        }
    }
}
