// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Net.Http;
using UrbanAirSharp.Dto;

namespace UrbanAirSharp.Request
{
	/// <summary>
	/// Used to form a VALIDATE request
	/// Accept the same range of payloads as /api/push, but parse and validate only, without sending any pushes
	/// 
	/// http://docs.urbanairship.com/reference/api/v3/push.html
	/// </summary>
	public class PushValidateRequest : PushRequest
	{
		public PushValidateRequest(Push push)
			: base(push)
		{
			RequestUrl = "api/push/validate/";
		}
	}
}
