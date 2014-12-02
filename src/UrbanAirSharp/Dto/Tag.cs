using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	public class Tag
	{
		[JsonIgnore]
		public String TagName;

		[JsonProperty("ios_channels")]
		public AddRemoveList IosChannels { get; set; }

		[JsonProperty("android_channels")]
		public AddRemoveList AndroidChannels { get; set; }

		[JsonProperty("device_tokens")]
		public AddRemoveList DeviceTokens { get; set; }

		[JsonProperty("device_pins")]
		public AddRemoveList DevicePins { get; set; }

		[JsonProperty("apids")]
		public AddRemoveList ApIds { get; set; }
	}
}
