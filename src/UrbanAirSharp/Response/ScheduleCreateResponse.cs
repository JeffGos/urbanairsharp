// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UrbanAirSharp.Dto;

namespace UrbanAirSharp.Response
{
	public class ScheduleCreateResponse : BaseResponse
	{
		[JsonProperty("schedule_urls")]
		public List<String> ScheduleUrls { get; set; }

		[JsonProperty("schedules")]
		public List<Schedule> Schedules { get; set; }
	}
}
