using System;
namespace botonera.Repository.HUD
{
    public interface IHud
    {
        void Show();
        void ShowInfo(string message);
        void ShowError(string message);
        void Dismiss();
    }
}
