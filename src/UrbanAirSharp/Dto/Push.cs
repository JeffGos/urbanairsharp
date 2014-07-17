// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UrbanAirSharp.Type;

namespace UrbanAirSharp.Dto
{
	public class Push
	{
		[JsonProperty("notification")]
		public Notification Notification { get; set; }

		private Audience _audience;

		[JsonProperty("audience")]
		public object Audience
		{
			get
			{
				if (_audience == null)
				{
					return "all";
				}

				return _audience;
			}
			set
			{
				var audience = value as Audience;
				_audience = audience;
			}
		}

		private IList<DeviceType> _deviceTypes;

		[JsonProperty("device_types")]
		public object DeviceTypes {
			get 
			{
				if (_deviceTypes == null)
				{
					return "all";
				}
					
				return _deviceTypes;
			}
			set
			{
				var list = value as IList<DeviceType>;
				_deviceTypes = list;
			}
		}

		[JsonProperty("options")]
		public Options Options { get; set; }

		//TODO: not implemented yet
		[JsonProperty("actions")]
		public Actions Actions { get; private set; }

		//TODO: not implemented yet
		[JsonProperty("message")]
		public RichMessage RichMessage { get; private set; }

		public void SetAudience(AudienceType audienceType, String value)
		{
			Audience = new Audience(audienceType, value);
		}
	}
}
