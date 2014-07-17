// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	public class AndroidAlert : BaseAlert
	{
		[JsonProperty("collapse_key")]
		public String CollapseKey { get; set; }

		[JsonProperty("time_to_live")]
		public int GcmTimeToLive { get; set; }

		[JsonProperty("delay_while_idle")]
		public bool DelayWhileIdle { get; set; }

		[JsonProperty("extra")]
		public Dictionary<String, String> Extras { get; set; }
	}
}