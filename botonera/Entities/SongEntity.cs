using System;
using Newtonsoft.Json;

namespace botonera.Entities
{
    public class SongEntity
    {
        [JsonProperty(PropertyName = "public")]
        public bool IsPublic { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "songCode")]
        public string SongCode { get; set; }

        [JsonProperty(PropertyName = "songName")]
        public string songName { get; set; }
    }
}
