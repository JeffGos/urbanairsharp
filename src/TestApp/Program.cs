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
		private const String AppKey = "HR_7_Oj1ROGwEvIY_vxNxQ";
		private const String AppMasterSecret = "OvAGMt-mT8y2NY1aqNP7Mw";

	    private const String TestDeviceToken = "MY_TEST_DEVICE_DEVICE_TOKEN";

	    private static UrbanAirSharpGateway _urbanAirSharpGateway;

        static void Main(String[] args)
        {
            _urbanAirSharpGateway = new UrbanAirSharpGateway(AppKey, AppMasterSecret);

	        testValidate();
	        testPush();
			testRegisterDevice();
			testTags();

            Console.ReadLine();
        }

		private static void testValidate()
		{
			Console.WriteLine("================ TESTING VALIDATE ================");
			Console.WriteLine();

			var response = _urbanAirSharpGateway.Validate("Validate push", new List<DeviceType>() { DeviceType.Android }, "946fdc3d-0284-468f-a2f7-d007ed694907");

			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();
		}

		private static void testPush()
		{
			Console.WriteLine("================ TESTING PUSH ================");
			Console.WriteLine();

			Console.WriteLine("PUSH Broadcast Alert");
			var response = _urbanAirSharpGateway.Push("Broadcast Alert");
			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();

			Console.WriteLine("PUSH Broadcast Alert to Androids");
			response = _urbanAirSharpGateway.Push("Broadcast Alert to Androids", new List<DeviceType>() { DeviceType.Android });
			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();

			Console.WriteLine("PUSH Targeted Alert to device");
			response = _urbanAirSharpGateway.Push("Targeted Alert to device", new List<DeviceType>() { DeviceType.Android }, "946fdc3d-0284-468f-a2f7-d007ed694907");
			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();

			Console.WriteLine("PUSH Custom Alert per device type");
			response = _urbanAirSharpGateway.Push("Custom Alert per device type", null, null, new List<BaseAlert>()
            {
                new AndroidAlert()
                {
                    Alert = "Custom Android Alert",
                    CollapseKey = "Collapse_Key",
                    DelayWhileIdle = true,
                    GcmTimeToLive = 5
                }
            });
			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();

			//these are just examples of tags
			var rugbyFanAudience = new Audience(AudienceType.Tag, "Rugby Fan");
			var footballFanAudience = new Audience(AudienceType.Tag, "Football Fan");
			var notFootballFanAudience = new Audience().NotAudience(footballFanAudience);
			var newZealandAudience = new Audience(AudienceType.Alias, "NZ");
			var englishAudience = new Audience(AudienceType.Tag, "language_en");

			var fansAudience = new Audience().OrAudience(new List<Audience>() { rugbyFanAudience, notFootballFanAudience });

			var customAudience = new Audience().AndAudience(new List<Audience>() { fansAudience, newZealandAudience, englishAudience });

			Console.WriteLine("PUSH to custom Audience");
			response = _urbanAirSharpGateway.Push("English speaking New Zealand Rugby fans", null, null, null, customAudience);

			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();
		}

		private static void testRegisterDevice()
	    {
			Console.WriteLine("================ TESTING REGISTERING A DEVICE TOKEN ================");
			Console.WriteLine();
			var response = _urbanAirSharpGateway.RegisterDeviceToken(TestDeviceToken);
			Console.WriteLine("Register Device Response: Ok?: {0}   Message: {1}  ErrorCode: {2}  ErrorMessage: {3}", 
				response.Ok, 
				response.Message, 
				response.ErrorCode, 
				response.Error);

			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();
	    }

		private static void testTags()
		{
			Console.WriteLine("================ TESTING TAGS ================");
			Console.WriteLine();

			var testTag = "some_tag";

			var tag = new Tag()
			{
				TagName = testTag,
				AndroidChannels = new AddRemoveList()
				{
					Add = new[] { "TEST_ANDROID_CHANNEL" }
				}
			};

			Console.WriteLine("CREATE TAG:");
			var response = _urbanAirSharpGateway.CreateTag(tag);
			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();

			Console.WriteLine("LIST TAGS:");
			var listResponse = _urbanAirSharpGateway.ListTags();
			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(listResponse.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();

			Console.WriteLine("DELETE TAG:");
			response = _urbanAirSharpGateway.DeleteTag(testTag);
			Console.Write(response.HttpResponseCode + " - ");
			Console.WriteLine(response.Ok ? "SUCCESS" : "FAILED");
			Console.WriteLine();
		}
    }
}
