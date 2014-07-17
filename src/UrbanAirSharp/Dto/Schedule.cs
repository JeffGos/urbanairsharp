// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	/// <summary>
	/// http://docs.urbanairship.com/reference/api/v3/schedule.html#schedule-object
	/// </summary>
	public class Schedule
	{
		[JsonProperty("name")]
		public String Name { get; set; }

		[JsonProperty("schedule")]
		public ScheduleInfo ScheduleInfo { get; set; }

		[JsonProperty("push")]
		public Push Push { get; set; }
	}
}
