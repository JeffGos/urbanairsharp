// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Net.Http;
using Newtonsoft.Json;


namespace UrbanAirSharp.Request
{
	public abstract class BaseRequest
	{
		[JsonIgnore] 
		public HttpMethod RequestMethod;

		[JsonIgnore]
		public String RequestUrl;

		public abstract object GetContent();
	}
}
