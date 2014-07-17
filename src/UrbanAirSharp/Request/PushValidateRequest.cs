// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System.Net.Http;

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
		public PushValidateRequest()
		{
			RequestUrl = "api/push/validate/";
			RequestMethod = HttpMethod.Post;
		}
	}
}
