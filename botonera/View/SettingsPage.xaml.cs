using System;
using System.Collections.Generic;
using botonera.Utils;
using Xamarin.Forms;

namespace botonera.View
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            OfflineSongsCell.On = PropertiesManager.PlayOnDevice;
            EndpointCell.Text = PropertiesManager.Endpoint;
        }

        void Handle_OfflineSwitchOnChanged(object sender, ToggledEventArgs e)
        {
            PropertiesManager.PlayOnDevice = e.Value;
        }

        void Handle_EndpointEntryCompleted(object sender, EventArgs e)
        {
            PropertiesManager.Endpoint = EndpointCell.Text;
        }
    }
}
