// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Net.Http;
using UrbanAirSharp.Dto;
using UrbanAirSharp.Request.Base;
using UrbanAirSharp.Response;

namespace UrbanAirSharp.Request
{
	/// <summary>
	/// Used to form a TAG delete request
	/// http://docs.urbanairship.com/api/ua.html#deleting-a-tag
	/// </summary>
	public class TagDeleteRequest : DeleteRequest<BaseResponse>
	{
		public TagDeleteRequest(String tag)
		{
			RequestUrl = "api/tags/" + tag + "/";
		}
	}
}
