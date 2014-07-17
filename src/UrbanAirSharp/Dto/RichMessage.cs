// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	/// <summary>
	/// title - Required
	/// body - Required
	/// content_type - optional MIME type. default is text/html
	/// content_encoding - optional. default is utf8
	/// extra - optional JSON String dictionary 
	/// icons - optional JSON String dictionary. Example : { "list_icon" : "ua:9bf2f510-050e-11e3-9446-14dae95134d2" }
	/// 
	///  http://docs.urbanairship.com/reference/api/v3/richpush.html#rich-push
	/// </summary>
	public class RichMessage
	{
		[JsonProperty("title")]
		public String Title { get; set; }

		[JsonProperty("body")]
		public String Body { get; set; }

		[JsonProperty("content_type")]
		public String ContentType { get; set; }

		[JsonProperty("content_encoding")]
		public String ContentEncoding { get; set; }

		[JsonProperty("extra")]
		public IDictionary<String,String> Extras { get; set; }

		[JsonProperty("icons")]
		public IDictionary<String, String> Icons { get; set; }
	}
}
