using botonera.ViewModel;
using Xamarin.Forms;
using botonera.View.SongList;
using botonera.Utils;

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
            localPlayActions = new LocalSongListActions();
            ConfigureSongList();
        }

        private async void ConfigureSongList()
        {
            SongList.ItemsSource = viewModel.Songs;
            SongList.ItemTapped += SongList_ItemTapped;
            await viewModel.UpdateSongs();
        }

        void SongList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(PropertiesManager.PlayOnDevice)
            {
                localPlayActions.SongList_ItemTapped(sender, e);
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
