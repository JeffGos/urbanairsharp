// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	public class ScheduleInfo
	{
		[JsonProperty("schedule_time")]
		public DateTime ScheduleTime { get; set; }
	}
}
