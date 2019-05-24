using botonera.Repository.HUD;
using botonera.UWP.Repositories.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

[assembly: Dependency(typeof(WindowsHud))]

namespace botonera.UWP.Repositories.HUD
{
    class WindowsHud : IHud
    {
        public void Dismiss()
        {
            //throw new NotImplementedException();
        }

        public void Show()
        {
            //throw new NotImplementedException();
        }

        public async void ShowError(string message)
        {
            ContentDialog errorDialog = new ContentDialog()
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "Ok"
            };

            await errorDialog.ShowAsync();
        }

        public async void ShowInfo(string message)
        {
            ContentDialog infoDialog = new ContentDialog()
            {
                Title = "Information",
                Content = message,
                CloseButtonText = "Ok"
            };

            await infoDialog.ShowAsync();
        }
    }
}
