using System;
using BigTed;
using botonera.iOS.Repositories.HUD;
using botonera.Repository.HUD;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSHud))]
namespace botonera.iOS.Repositories.HUD
{
    public class IOSHud: IHud
    {
        public void Dismiss()
        {
            BTProgressHUD.Dismiss();
        }

        public void Show()
        {
            BTProgressHUD.Show(maskType: ProgressHUD.MaskType.Gradient);
        }

        public void ShowError(string message)
        {
            BTProgressHUD.ShowErrorWithStatus(message);
        }

        public void ShowInfo(string message)
        {
            BTProgressHUD.Show(message, maskType: ProgressHUD.MaskType.Gradient);
        }
    }
}
