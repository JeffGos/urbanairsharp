// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Collections.Generic;
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

		[JsonProperty("push_ids")]
		public List<String> PushIds { get; set; }

		private String _url;

		/// <summary>
		/// Set by server in responses, ignored in requests
		/// </summary>
		[JsonProperty("url")]
		public String Url
		{
			get { return _url; }
			set
			{
				_url = value;

				if (_url == null || _url.Length < 36)
				{
					return;
				}

				var id = _url.Substring(_url.Length - 36);
				Id = Guid.Parse(id);
			} 
		}

		/// <summary>
		/// Only set if Url is set by server in responses
		/// </summary>
		[JsonIgnore] public Guid Id;
	}
}
