// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using UrbanAirSharp.Request.Base;
using UrbanAirSharp.Response;

namespace UrbanAirSharp.Request
{
	/// <summary>
	/// Used to form a TAG listing request
	/// http://docs.urbanairship.com/api/ua.html#get--api-tags-
	/// </summary>
	public class TagListRequest : GetRequest<TagListResponse>
	{
		public TagListRequest()
		{
			RequestUrl = "api/tags/";
		}
	}
}
