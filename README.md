urbanairsharp
=============

A C# .Net wrapper library to simplify server-side calls to the Urban Airship API

# Getting Started

The source includes a test project which has examples on the supported functionality of the library

Using the library is very easy. Simply supply your UA keys and call Push

    var client = new UrbanAirSharpGateway(AppKey, AppMasterSecret);

	client.Push("Hey there - here's a Broadcast");

Here are some more examples of the supported functionality

    client.Validate("Validate push", new List<DeviceType>() { DeviceType.Android }, "946fdc3d-0284-468f-a2f7-d007ed694907"); 

	client.Push("Broadcast Alert");

	client.Push("Broadcast Alert to Androids", new List<DeviceType>() { DeviceType.Android });

	client.Push("Targeted Alert to device", new List<DeviceType>() { DeviceType.Android }, "946fdc3d-0284-468f-a2f7-d007ed694907");

This is an example of a more complicated, audience targeted Push
	
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

