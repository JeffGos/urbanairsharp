// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UrbanAirSharp.Dto;

namespace UrbanAirSharp.Response
{
	public class ScheduleGetResponse : BaseResponse
	{
		[JsonProperty("name")]
		public String Name { private get; set; }

		[JsonProperty("schedule")]
		public ScheduleInfo ScheduleInfo { private get; set; }

		[JsonProperty("push")]
		public Push Push { private get; set; }

		[JsonProperty("push_ids")]
		public List<String> PushIds { private get; set; }

		[JsonIgnore]
		public Schedule Schedule;

		/// <summary>
		/// Set by server in responses, ignored in requests
		/// </summary>
		[JsonProperty("url")]
		public String Url { private get; set; }

		public override void OnDeserialised()
		{
			Schedule = new Schedule()
			{
				Name = Name,
				Url = Url, 
				ScheduleInfo = ScheduleInfo,
				Push = Push
			};
		}
	}
}
