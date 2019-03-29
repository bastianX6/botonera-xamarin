using System;
using Newtonsoft.Json;

namespace botonera.Entities
{
    public class SongResponseEntity
    {
        [JsonProperty(PropertyName = "songs")]
        public SongEntity[] Songs { get; set; }
    }
}
