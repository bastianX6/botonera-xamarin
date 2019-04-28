using System;
using System.Collections.Generic;
using botonera.Repository.FileDownload;
using botonera.Utils;
using Xamarin.Forms;

namespace botonera.View
{
    public partial class SettingsPage : ContentPage
    {
        FirebaseStorageFileManager manager;
        public SettingsPage()
        {
            InitializeComponent();
            OfflineSongsCell.On = PropertiesManager.PlayOnDevice;
            EndpointCell.Text = PropertiesManager.Endpoint;
            manager = new FirebaseStorageFileManager();
        }

        void Handle_OfflineSwitchOnChanged(object sender, ToggledEventArgs e)
        {
            PropertiesManager.PlayOnDevice = e.Value;
        }

        void Handle_EndpointEntryCompleted(object sender, EventArgs e)
        {
            PropertiesManager.Endpoint = EndpointCell.Text;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            bool answer = await DisplayAlert("Are you sure?", "You'll delete all song files", "Delete all", "Cancel");
            if (!answer) return;

            bool deleteSuccess = manager.DeleteAllFiles();

            var message = deleteSuccess ? "Files deleted successfully" : "Error deleting songs. Try again";
            await DisplayAlert("Information", message, "OK");
        }
    }
}
