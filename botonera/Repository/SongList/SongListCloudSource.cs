using System;
using System.Net.Http;
using System.Threading.Tasks;
using botonera.Entities;
using Newtonsoft.Json;

namespace botonera.Repository.SongList
{
    public class SongListCloudSource: ISongListDataSource
    {
        public async Task<SongResponseEntity> GetSongList()
        {
            var url = "https://us-central1-raspberry-instants.cloudfunctions.net/botonera/song/list";
            var client = new HttpClient();
            var result = await client.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<SongResponseEntity>(result);
            return response;
        }
    }
}
