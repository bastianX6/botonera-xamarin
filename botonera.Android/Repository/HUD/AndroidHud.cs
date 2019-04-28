using System;
using Android.Content;
using AndroidHUD;
using botonera.Droid.Repository.HUD;
using botonera.Repository.HUD;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidHud))]
namespace botonera.Droid.Repository.HUD
{
    public class AndroidHud: IHud
    {

        public void Dismiss()
        {
            AndHUD.Shared.Dismiss();
        }

        public void Show()
        {
            AndHUD.Shared.Show(CrossCurrentActivity.Current.Activity);
        }

        public void ShowError(string message)
        {
            AndHUD.Shared.ShowError(CrossCurrentActivity.Current.Activity, message, timeout: new TimeSpan(0,0,1));
        }

        public void ShowInfo(string message)
        {
            AndHUD.Shared.Show(CrossCurrentActivity.Current.Activity, message);
        }
    }
}
