// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System.Net.Http;
using UrbanAirSharp.Dto;

namespace UrbanAirSharp.Request
{
	/// <summary>
	/// Used to form a SCHEDULE request
	/// 
	/// http://docs.urbanairship.com/reference/api/v3/schedule.html#schedule-a-notification
	/// </summary>
	public class ScheduleCreateRequest : BaseRequest
	{
		public Schedule Content { get; set; }

		public ScheduleCreateRequest()
		{
			RequestUrl = "api/schedules/";
			RequestMethod = HttpMethod.Post;
		}

		public override object GetContent()
		{
			return Content;
		}
	}
}
