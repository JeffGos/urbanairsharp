// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UrbanAirSharp.Dto;
using UrbanAirSharp.Request;
using UrbanAirSharp.Response;
using UrbanAirSharp.Type;

namespace UrbanAirSharp
{
	/// <summary>
	/// A gateway for pushing notifications to the Urban Airship API V3
	/// http://docs.urbanairship.com/reference/api/v3/
	/// 
	/// Supported:
	/// ---------
	/// api/push
	/// api/push/validate
	/// 
	/// Not Supported Yet:
	/// -----------------
	/// api/schedule 
	/// api/tags 
	/// api/feeds 
	/// api/reports 
	/// api/device_tokens 
	/// api/segments 
	/// api/location 
	/// </summary>

	public class UrbanAirSharpGateway
	{
		private const String UrbanAirShipBaseUrl = "https://go.urbanairship.com/";

		private String _appKey;
		private String _appMasterSecret;
		private readonly HttpClient _httpClient;

		private static readonly ILog Log = LogManager.GetLogger(typeof(UrbanAirSharpGateway));

		public UrbanAirSharpGateway(String appKey, String appMasterSecret)
		{
			_appKey = appKey;
			_appMasterSecret = appMasterSecret;
			_httpClient = new HttpClient();

			XmlConfigurator.Configure();

			var auth = String.Format("{0}:{1}", _appKey, _appMasterSecret);
			auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
			_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/vnd.urbanairship+json; version=3;");
		}

		/// <summary>
		/// - Broadcast to all devices
		/// - Broadcast to one device type
		/// - Send to a targeted device
		/// - Broadcast to all devices with a different alert for each type
		/// </summary>
		/// <param name="alert">The message to be pushed</param>
		/// <param name="deviceTypes">use null for broadcast</param>
		/// <param name="deviceId">use null for broadcast or deviceTypes must contain 1 element that distinguishes this deviceId</param>
		/// <param name="deviceAlerts">per device alert messages and extras</param>
		/// <param name="customAudience">a more specific way to choose the audience for the push. If this is set, deviceId is ignored</param>
		/// <returns></returns>
		public BaseResponse Push(String alert, IList<DeviceType> deviceTypes = null, String deviceId = null, IList<BaseAlert> deviceAlerts = null, Audience customAudience = null)
		{
			var request = new PushRequest()
			{
				Content = CreatePush(alert, deviceTypes, deviceId, deviceAlerts, customAudience)
			};

			var httpResponseMessage = SendRequest(request);

			return new BaseResponse()
			{
				HttpResponseCode = httpResponseMessage.Result.StatusCode
			};
		}

		/// <summary>
		/// Validates a push request. Duplicates Push without actually sending the alert. See Push
		/// </summary>
		/// <param name="alert">The message to be pushed</param>
		/// <param name="deviceTypes">use null for broadcast</param>
		/// <param name="deviceId">use null for broadcast or deviceTypes must contain 1 element that distinguishes this deviceId</param>
		/// <param name="deviceAlerts">per device alert messages and extras</param>
		/// <param name="customAudience">a more specific way to choose the audience for the push. If this is set, deviceId is ignored</param>
		/// <returns></returns>
		public BaseResponse Validate(String alert, IList<DeviceType> deviceTypes = null, String deviceId = null,
			IList<BaseAlert> deviceAlerts = null, Audience customAudience = null)
		{
			var request = new PushValidateRequest()
			{
				Content = CreatePush(alert, deviceTypes, deviceId, deviceAlerts, customAudience)
			};

			var httpResponseMessage = SendRequest(request);

			return new BaseResponse()
			{
				HttpResponseCode = httpResponseMessage.Result.StatusCode
			};
		}

		public BaseResponse AddSchedule(String alert, DateTime triggerDate)
		{
			//TODO:
			return null;
		}

		public BaseResponse EditSchedule(Guid scheduleId, String alert, DateTime triggerDate)
		{
			//TODO:
			return null;
		}

		public BaseResponse DeleteSchedule(Guid scheduleId)
		{
			//TODO:
			return null;
		}

		public BaseResponse GetSchedule(Guid scheduleId)
		{
			//TODO:
			return null;
		}

		public BaseResponse ListSchedules()
		{
			//TODO:
			return null;
		}

		private static Push CreatePush(String alert, IList<DeviceType> deviceTypes = null, String deviceId = null, IList<BaseAlert> deviceAlerts = null, Audience customAudience = null)
		{
			var push = new Push()
			{
				Notification = new Notification()
				{
					DefaultAlert = alert
				}
			};

			if (customAudience != null)
			{
				deviceId = null;

				push.Audience = customAudience;
			}

			if (deviceTypes != null)
			{
				push.DeviceTypes = deviceTypes;

				if (deviceId != null)
				{
					if (deviceTypes.Count != 1)
					{
						throw new InvalidOperationException("when deviceId is not null, deviceTypes must contain 1 element which identifies the deviceId type");
					}

					var deviceType = deviceTypes[0];

					switch (deviceType)
					{
						case DeviceType.Android:
							push.SetAudience(AudienceType.Android, deviceId);
							break;
						case DeviceType.Ios:
							push.SetAudience(AudienceType.Ios, deviceId);
							break;
						case DeviceType.Wns:
							push.SetAudience(AudienceType.Windows, deviceId);
							break;
						case DeviceType.Mpns:
							push.SetAudience(AudienceType.WindowsPhone, deviceId);
							break;
						case DeviceType.Blackberry:
							push.SetAudience(AudienceType.Blackberry, deviceId);
							break;
					}
				}
			}

			if (deviceAlerts == null || deviceAlerts.Count <= 0)
			{
				return push;
			}

			push.Notification.AndroidAlert = (AndroidAlert)deviceAlerts.FirstOrDefault(x => x is AndroidAlert);
			push.Notification.IosAlert = (IosAlert)deviceAlerts.FirstOrDefault(x => x is IosAlert);
			push.Notification.WindowsAlert = (WindowsAlert)deviceAlerts.FirstOrDefault(x => x is WindowsAlert);
			push.Notification.WindowsPhoneAlert = (WindowsPhoneAlert)deviceAlerts.FirstOrDefault(x => x is WindowsPhoneAlert);
			push.Notification.BlackberryAlert = (BlackberryAlert)deviceAlerts.FirstOrDefault(x => x is BlackberryAlert);

			return push;
		}

		private Task<HttpResponseMessage> SendRequest(BaseRequest request)
		{
			var settings = new JsonSerializerSettings();
			settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
			settings.NullValueHandling = NullValueHandling.Ignore;

			var json = JsonConvert.SerializeObject(request.GetContent(), settings);
			var url = UrbanAirShipBaseUrl + request.RequestUrl;

			Log.Debug(url);
			Log.Debug(json);

			var httpResponseMessage = _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

			Log.Debug("");
			Log.Debug(httpResponseMessage.Result);

			return httpResponseMessage;
		}
	}
}
