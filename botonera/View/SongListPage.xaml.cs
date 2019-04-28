using botonera.ViewModel;
using Xamarin.Forms;
using botonera.View.SongList;
using botonera.Utils;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using System;

namespace botonera.View
{
    public partial class SongListPage : ContentPage
    {
        SongListViewModel viewModel;
        ISongListActions remotePlayActions;
        ISongListActions localPlayActions;

        public SongListPage()
        {
            InitializeComponent();
            viewModel = new SongListViewModel();
            remotePlayActions = new RemoteSongListActions(viewModel);
            localPlayActions = new LocalSongListActions(viewModel);
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
            if(PropertiesManager.PlayOnDevice)
            {
                var permission = Permission.Storage;
                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                        {
                            await DisplayAlert("Need permission", "Please grant storage permission", "OK");
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                        //Best practice to always check that the key exists
                        if (results.ContainsKey(permission))
                            status = results[permission];
                    }

                    if (status == PermissionStatus.Granted)
                    {
                        localPlayActions.SongList_ItemTapped(sender, e);
                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                        await DisplayAlert("Storage Denied", "Can't play song, try again.", "OK");
                    }
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine($"Error requesting permissions: {ex}");
                }
            }
            else
            {
                remotePlayActions.SongList_ItemTapped(sender, e);
            }

        }

        void ButtonStop_Clicked(object sender, System.EventArgs e)
        {
            if (PropertiesManager.PlayOnDevice)
            {
                localPlayActions.ButtonStop_Clicked(sender, e);
            }
            else
            {
                remotePlayActions.ButtonStop_Clicked(sender, e);
            }
        }

        void ButtonClock_Clicked(object sender, System.EventArgs e)
        {
            if (PropertiesManager.PlayOnDevice)
            {
                localPlayActions.ButtonClock_Clicked(sender, e);
            }
            else
            {
                remotePlayActions.ButtonClock_Clicked(sender, e);
            }
        }

        void ButtonRandom_Clicked(object sender, System.EventArgs e)
        {
            if (PropertiesManager.PlayOnDevice)
            {
                localPlayActions.ButtonRandom_Clicked(sender, e);
            }
            else
            {
                remotePlayActions.ButtonRandom_Clicked(sender, e);
            }
        }
    }
}
