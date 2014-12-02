// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	/// <summary>
	/// Info about the schedule
	/// Set one of the two times below
	/// Times should be in UTC
	/// </summary>
	public class ScheduleInfo
	{
		/// <summary>
		/// Scheduled push to be delivered globally at the same moment
		/// </summary>
		[JsonProperty("scheduled_time")]
		public DateTime? ScheduleTime { get; set; }

		/// <summary>
		/// Scheduled push to be delivered at the device local time
		/// </summary>
		[JsonProperty("local_scheduled_time")]
		public DateTime? LocalScheduleTime { get; set; }
	}
}
