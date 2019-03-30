using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace botonera.Repository.PlaySong
{
    public class PlaySongCloudSource: IPlaySongDataSource
    {
        public async Task<HttpStatusCode> PlaySong(string endpoint, string songCode)
        {
            var url = $"{endpoint}/play/{songCode}";
            var client = new HttpClient();
            var result = await client.GetAsync(url);
            return result.StatusCode;
        }

        public async Task<HttpStatusCode> PlayClock(string endpoint)
        {
            var url = $"{endpoint}/clock";
            var client = new HttpClient();
            var result = await client.GetAsync(url);
            return result.StatusCode;
        }

        public async Task<HttpStatusCode> Stop(string endpoint)
        {
            var url = $"{endpoint}/stop";
            var client = new HttpClient();
            var result = await client.GetAsync(url);
            return result.StatusCode;
        }
    }
}
