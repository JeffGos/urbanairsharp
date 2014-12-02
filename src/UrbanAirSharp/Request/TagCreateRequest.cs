// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Net.Http;
using UrbanAirSharp.Dto;
using UrbanAirSharp.Request.Base;
using UrbanAirSharp.Response;

namespace UrbanAirSharp.Request
{
	/// <summary>
	/// Used to form a TAG create request
	/// http://docs.urbanairship.com/api/ua.html#put--api-tags-(tag)
	/// </summary>
	public class TagCreateRequest : PutRequest<BaseResponse, Tag>
	{
		public TagCreateRequest(Tag tag)
			: base(tag)
		{
			RequestUrl = "api/tags/" + tag.TagName + "/";
		}
	}
}
