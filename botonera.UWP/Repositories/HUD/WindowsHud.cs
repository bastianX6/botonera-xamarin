using botonera.Repository.HUD;
using botonera.UWP.Repositories.HUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void ShowError(string message)
        {
            //throw new NotImplementedException();
        }

        public void ShowInfo(string message)
        {
            //throw new NotImplementedException();
        }
    }
}
