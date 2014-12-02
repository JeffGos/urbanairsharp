// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
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

		[JsonProperty("error")]
		public String Error { get; set; }

		[JsonProperty("error_code")]
		public int ErrorCode { get; set; }

		[JsonIgnore]
		public HttpStatusCode HttpResponseCode { get; set; }

		[JsonIgnore]
		public String Message { get; set; }

		/// <summary>
		/// Override this in derived classes to perform any post processing
		/// on the object after it has been deserialised from JSON
		/// </summary>
		public virtual void OnDeserialised()
		{
			
		}
	}
}
