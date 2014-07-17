// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace UrbanAirSharp.Response
{
	public class BaseResponse
	{
		[JsonProperty("ok")]
		public bool Ok { get; set; }

		[JsonProperty("operation_id")]
		public Guid OperationId { get; set; }

		[JsonProperty("push_ids")]
		public List<Guid> PushIds { get; set; }

		[JsonProperty("message_ids")]
		public List<Guid> MessageIds { get; set; }

		[JsonProperty("url_ids")]
		public List<Guid> UrlIds { get; set; }

		[JsonProperty("content_urls")]
		public List<String> ContentUrls { get; set; }

		[JsonProperty("error")]
		public String Error { get; set; }

		[JsonProperty("error_code")]
		public int ErrorCode { get; set; }

		[JsonIgnore]
		public HttpStatusCode HttpResponseCode { get; set; }

		[JsonIgnore]
		public String Message { get; set; }
	}
}
