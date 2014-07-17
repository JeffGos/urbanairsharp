// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace UrbanAirSharp.Response
{
	public class BaseResponse : HttpResponseMessage
	{
		[JsonProperty("operation_id")]
		public Guid OperationId { get; set; }

		[JsonProperty("push_ids")]
		public List<Guid> PushIds { get; set; }

		[JsonProperty("ok")]
		public bool Ok { get; set; }

		[JsonIgnore]
		public HttpStatusCode HttpResponseCode { get; set; }
	}
}
