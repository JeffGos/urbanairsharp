using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
    public class DeviceToken
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("badge")]
        public int Badge { get; set; }

        [JsonProperty("quiettime")]
        public QuietTime QuietTime { get; set; }

        [JsonProperty("tz")]
        public string Timezone { get; set; }

        [JsonIgnore]
        public string Token { get; set; }
    }

    public class QuietTime
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }
    }
}
