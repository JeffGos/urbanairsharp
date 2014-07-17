// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Collections.Generic;
using UrbanAirSharp;
using UrbanAirSharp.Dto;
using UrbanAirSharp.Type;

namespace TestApp
{
	class Program
	{
		private const String AppKey = "WruqqOeaTlSKSRCUJrzA0g";
		private const String AppMasterSecret = "GcXgFOl5QSKgMfeYZWPb_w";

		static void Main(String[] args)
		{
			var client = new UrbanAirSharpGateway(AppKey, AppMasterSecret);

			client.Validate("Validate push", new List<DeviceType>() { DeviceType.Android }, "946fdc3d-0284-468f-a2f7-d007ed694907");

			client.Push("Broadcast Alert");

			client.Push("Broadcast Alert to Androids", new List<DeviceType>() { DeviceType.Android });

			client.Push("Targeted Alert to device", new List<DeviceType>() { DeviceType.Android }, "946fdc3d-0284-468f-a2f7-d007ed694907");
			
			client.Push("Custom Alert per device type", null, null, new List<BaseAlert>()
			{
				new AndroidAlert()
				{
					Alert = "Custom Android Alert",
					CollapseKey = "Collapse_Key",
					DelayWhileIdle = true,
					GcmTimeToLive = 5
				}
			});

			//these are just examples of tags
			var rugbyFanAudience = new Audience(AudienceType.Tag, "Rugby Fan");
			var footballFanAudience = new Audience(AudienceType.Tag, "Football Fan");
			var notFootballFanAudience = new Audience().NotAudience(footballFanAudience);
			var newZealandAudience = new Audience(AudienceType.Alias, "NZ");
			var englishAudience = new Audience(AudienceType.Tag, "language_en");

			var fansAudience = new Audience().OrAudience(new List<Audience>() { rugbyFanAudience, notFootballFanAudience });

			var customAudience = new Audience().AndAudience(new List<Audience>() { fansAudience, newZealandAudience, englishAudience });

			client.Push("English speaking New Zealand Rugby fans", null, null, null, customAudience);

			Console.ReadLine();
		}
	}
}
