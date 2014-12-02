// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UrbanAirSharp.Dto;

namespace UrbanAirSharp.Response
{
	public class ScheduleListResponse : BaseResponse
	{
		[JsonProperty("count")]
		public int? Count { get; set; }

		[JsonProperty("total_count")]
		public int? Total { get; set; }

		[JsonProperty("next_page")]
		public String NextPageUrl { get; set; }

		[JsonProperty("schedules")]
		public IList<Schedule> Schedules { get; set; }
	}
}
