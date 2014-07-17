// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System.Net.Http;
using UrbanAirSharp.Dto;

namespace UrbanAirSharp.Request
{
	/// <summary>
	/// Used to form a PUSH request
	/// Send a push notification to a specified device or list of devices
	/// 
	/// audience - Required
	/// notification - Required
	/// device_types - Required
	/// 
	/// options - optionally specify an expiry date
	/// actions - TODO not supported yet 
	/// message - RICH PUSH message - TODO not supported yet
	/// 
	/// http://docs.urbanairship.com/reference/api/v3/push.html
	/// </summary>
	public class PushRequest : BaseRequest
	{
		public Push Content { get; set; }

		public PushRequest()
		{
			RequestUrl = "api/push/";
			RequestMethod = HttpMethod.Post;
		}

		public override object GetContent()
		{
			return Content;
		}
	}
}
