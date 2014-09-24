// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Collections.Generic;
using System.IO;
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
using UrbanAirSharp.Request.Base;
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
    /// api/schedule 
    /// 
    /// Not Supported Yet:
    /// -----------------
    /// api/tags 
    /// api/feeds 
    /// api/reports 
    /// api/device_tokens 
    /// api/segments 
    /// api/location 
    /// </summary>

    public class UrbanAirSharpGateway
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UrbanAirSharpGateway));

        public UrbanAirSharpGateway(String appKey, String appMasterSecret)
        {
            XmlConfigurator.Configure();
            ServiceModelConfig.Create(appKey, appMasterSecret);
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
            var request = new PushRequest(CreatePush(alert, deviceTypes, deviceId, deviceAlerts, customAudience));
            
            var response = request.ExecuteAsync();

            return response.Result;
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
            var request = new PushValidateRequest(CreatePush(alert, deviceTypes, deviceId, deviceAlerts, customAudience));

            var response = request.ExecuteAsync();

            return response.Result;
        }

        public BaseResponse ScheduleAdd(String alert, DateTime triggerDate)
        {
            //TODO:
            return null;
        }

        public BaseResponse ScheduleEdit(Guid scheduleId, String alert, DateTime triggerDate)
        {
            //TODO:
            return null;
        }

        public BaseResponse ScheduleDelete(Guid scheduleId)
        {
            //TODO:
            return null;
        }

        public BaseResponse ScheduleGet(Guid scheduleId)
        {
            //TODO:
            return null;
        }

        public BaseResponse SchedulesList()
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


        /// <summary>
        /// Registers a device token only with the Urban Airship site, this can be used for new device tokens and for existing tokens.
        /// The exitsing settings (badge, tags, alias, quiet times) will be overriden. If a token has become inactive reregistering it
        /// will make it active again.
        /// </summary>
        /// <returns>Response from Urban Airship</returns>
        public BaseResponse RegisterDeviceToken(string deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken))
                throw new ArgumentException("A device Token is Required", "deviceToken");

            var deviceRequest = new DeviceTokenRequest(new DeviceToken() {Token = deviceToken});
            var requestTask = deviceRequest.ExecuteAsync();

            return requestTask.Result;
        }

        /// <summary>
        /// Registers a device token with extended properties with the Urban Airship site, this can be used for new device 
        /// tokens and for existing tokens. If a token has become inactive reregistering it will make it active again. 
        /// </summary>
        /// <returns>Response from Urban Airship</returns>
        public BaseResponse RegisterDeviceToken(DeviceToken deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken.Token))
                throw new ArgumentException("A device Tokens Token field is Required", "deviceToken");

            var deviceRequest = new DeviceTokenRequest(deviceToken);
            var requestTask = deviceRequest.ExecuteAsync();

            return requestTask.Result;
        }
    }
}
