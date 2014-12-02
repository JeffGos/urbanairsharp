// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UrbanAirSharp.Dto
{
	public class AddRemoveList
	{
		[JsonProperty("add")]
		public IList<String> Add { get; set; }

		[JsonProperty("remove")]
		public IList<String> Remove { get; set; }
	}
}
