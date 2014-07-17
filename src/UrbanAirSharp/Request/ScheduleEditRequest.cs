// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Net.Http;
using UrbanAirSharp.Dto;

namespace UrbanAirSharp.Request
{
	/// <summary>
	/// Used to form a SCHEDULE request
	/// 
	/// http://docs.urbanairship.com/reference/api/v3/schedule.html#schedule-a-notification
	/// </summary>
	public class ScheduleEditRequest : BaseRequest
	{
		public Schedule Content { get; set; }
		
		public ScheduleEditRequest(Guid scheduleId)
		{
			RequestUrl = "api/schedules/" + scheduleId;
			RequestMethod = HttpMethod.Post;
		}

		public override object GetContent()
		{
			return Content;
		}
	}
}
