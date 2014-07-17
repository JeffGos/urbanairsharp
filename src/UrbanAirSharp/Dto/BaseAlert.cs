// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	public class BaseAlert
	{
		[JsonProperty("alert")]
		public String Alert { get; set; }
	}
}