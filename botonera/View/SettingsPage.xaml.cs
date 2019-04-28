using System;
using botonera.Repository.FileDownload;
using botonera.Utils;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
                    DeleteFiles();
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

        private async void DeleteFiles()
        {
            bool answer = await DisplayAlert("Are you sure?", "You'll delete all song files", "Delete all", "Cancel");
            if (!answer) return;

            bool deleteSuccess = manager.DeleteAllFiles();

            var message = deleteSuccess ? "Files deleted successfully" : "Error deleting songs. Try again";
            await DisplayAlert("Information", message, "OK");
        }
    }
}
