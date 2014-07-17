// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	public class OpenAction
	{
		//either url, deep_link or landing_page
		//TODO: landing_page not supported yet
		[JsonProperty("type")]
		public String Type { get; set; }

		//TODO: landing_page content not supported yet
		//http://docs.urbanairship.com/reference/api/v3/actions.html#open
		[JsonProperty("content")]
		public String Content { get; set; }
	}
}
