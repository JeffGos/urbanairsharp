// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using UrbanAirSharp.Response;


namespace UrbanAirSharp.Request.Base
{
	public abstract class BaseRequest
	{
		protected HttpClient HttpClient;
		protected JsonSerializerSettings SerializerSettings;
		protected String Host;
		protected String RequestUrl;
		protected HttpMethod RequestMethod;

		protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseRequest));

		protected BaseRequest(String host, HttpClient httpClient, JsonSerializerSettings serializerSettings)
		{
			Host = host;
			HttpClient = httpClient;
			SerializerSettings = serializerSettings;

			if (!Host.EndsWith("/"))
			{
				Host += "/";
			}
		}

		public virtual async Task<BaseResponse> ExecuteAsync()
		{
			return null;
		}

		protected async Task<BaseResponse> DeserializeResponseAsync(HttpResponseMessage response)
		{
			var contentJson = await response.Content.ReadAsStringAsync();

			Log.Debug("Response - " + response.StatusCode + " - " + contentJson);

			var result = JsonConvert.DeserializeObject<BaseResponse>(contentJson);

			result.Message = response.ReasonPhrase;
			result.HttpResponseCode = response.StatusCode;

			return result;
		}
	}
}
