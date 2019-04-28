using System;
using Xamarin.Forms;

namespace botonera.View.SongList
{
    public interface ISongListActions
    {
        void SongList_ItemTapped(object sender, ItemTappedEventArgs e);
        void ButtonStop_Clicked(object sender, EventArgs e);
        void ButtonClock_Clicked(object sender, EventArgs e);
        void ButtonRandom_Clicked(object sender, EventArgs e);
    }
}
