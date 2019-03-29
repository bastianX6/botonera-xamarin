using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using botonera.Repository.SongList;
using Xamarin.Forms;

namespace botonera
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var dataSource = new SongListDataSource();
            var data = await dataSource.GetSongList();
            System.Diagnostics.Debug.WriteLine(data);
        }
    }
}
