// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UrbanAirSharp.Response
{
	public class PushResponse : BaseResponse
	{
		[JsonProperty("push_ids")]
		public List<Guid> PushIds { get; set; }

		[JsonProperty("message_ids")]
		public List<Guid> MessageIds { get; set; }

		[JsonProperty("url_ids")]
		public List<Guid> UrlIds { get; set; }

		[JsonProperty("content_urls")]
		public List<String> ContentUrls { get; set; }
	}
}
