// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using UrbanAirSharp.Response;


namespace UrbanAirSharp.Request.Base
{
	public abstract class BaseRequest<TResponse> where TResponse : BaseResponse, new()
	{
		protected HttpClient HttpClient;
		protected JsonSerializerSettings SerializerSettings;
		protected String Host;
		protected String RequestUrl;
		protected HttpMethod RequestMethod;

		protected static readonly ILog Log = LogManager.GetLogger(typeof(TResponse));

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

		public virtual async Task<TResponse> ExecuteAsync()
		{
			return null;
		}

		protected async Task<TResponse> DeserializeResponseAsync(HttpResponseMessage response)
		{
			var contentJson = await response.Content.ReadAsStringAsync();

			Log.Info("Response - (" + response.StatusCode + ") - " + contentJson);

			TResponse result;

			try
			{
				result = JsonConvert.DeserializeObject<TResponse>(contentJson);
			}
			catch (Exception e)
			{
				//Some calls to Urban Airship don't return with valid JSON :(
				Log.Debug("DeserializeResponseAsync - The server did not respond with valid JSON", e);
				result = new TResponse
				{
					Message = "The server did not response with proper JSON (" + contentJson + ")"
				};
			}

			if (result == null)
			{
				result = new TResponse();
			}

			if (String.IsNullOrEmpty(result.Message))
			{
				result.Message = response.ReasonPhrase;
			}

			result.HttpResponseCode = response.StatusCode;

			if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted ||
					response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.NoContent)
			{
				result.Ok = true;
			}

			result.OnDeserialised();

			return result;
		}
	}
}
