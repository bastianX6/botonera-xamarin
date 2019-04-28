using System;
using System.Collections.Generic;
using botonera.Entities;
using botonera.Repository.FileDownload;
using botonera.ViewModel;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace botonera.View.SongList
{
    public class RemoteSongListActions: ISongListActions
    {
        SongListViewModel viewModel;
        public RemoteSongListActions(SongListViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public async void SongList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var song = e.Item as SongEntity;
                var success = await viewModel.PlaySong(song.SongCode);
                System.Diagnostics.Debug.WriteLine($"Success: {success} Song Code: {song.SongCode} | Song Description: {song.Description}");
                Analytics.TrackEvent("Song clicked", new Dictionary<string, string> {
                    { "SongCode", $"{song.SongCode}" },
                    { "SongDescription", $"{song.Description}"}
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on song play: {ex}");
                Analytics.TrackEvent("Song clicked error", new Dictionary<string, string> {
                    { "Description", $"{ex.Message}" }
                });
            }

        }

        public async void ButtonStop_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var success = await viewModel.Stop();
                Analytics.TrackEvent("Stop clicked");
                System.Diagnostics.Debug.WriteLine($"Stop songs success: {success}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on song stop: {ex}");
                Analytics.TrackEvent("Stop Song error", new Dictionary<string, string> {
                    { "Description", $"{ex.Message}" }
                });
            }
        }

        public async void ButtonClock_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var success = await viewModel.PlayClock();
                Analytics.TrackEvent("Clock clicked");
                System.Diagnostics.Debug.WriteLine($"Clock success: {success}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on clock song: {ex}");
                Analytics.TrackEvent("Clock clicked error", new Dictionary<string, string> {
                    { "Description", $"{ex.Message}" }
                });
            }
        }

        public async void ButtonRandom_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var random = new Random();
                var index = random.Next(0, viewModel.Songs.Count - 1);
                var song = viewModel.Songs[index];
                var success = await viewModel.PlaySong(song.SongCode);
                System.Diagnostics.Debug.WriteLine($"Success: {success} Song Code: {song.SongCode} | Song Description: {song.Description}");
                Analytics.TrackEvent("Random Song clicked", new Dictionary<string, string> {
                    { "SongCode", $"{song.SongCode}" },
                    { "SongDescription", $"{song.Description}"}
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error on random song play: {ex}");
                Analytics.TrackEvent("Random Song clicked error", new Dictionary<string, string> {
                    { "Description", $"{ex.Message}" }
                });
            }
        }
    }
}
