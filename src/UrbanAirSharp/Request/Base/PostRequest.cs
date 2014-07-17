// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanAirSharp.Response;

namespace UrbanAirSharp.Request.Base
{
	/// <summary>
	/// Used to form a VALIDATE request
	/// Accept the same range of payloads as /api/push, but parse and validate only, without sending any pushes
	/// 
	/// http://docs.urbanairship.com/reference/api/v3/push.html
	/// </summary>
	public class PostRequest <T> : BaseRequest
	{
		//TODO: PostRequest shouldn't declate this - should be more abstract
		public readonly Encoding Encoding = Encoding.UTF8;
		public const String MediaType = "application/json";

		protected T Content;
		
		public PostRequest(T content)
			: base(ServiceModelConfig.Host, ServiceModelConfig.HttpClient, ServiceModelConfig.SerializerSettings)
		{
			RequestMethod = HttpMethod.Post;
			Content = content;
		}

		public override async Task<BaseResponse> ExecuteAsync()
		{
			Log.Debug(RequestMethod + " - " + Host + RequestUrl);

			var json = JsonConvert.SerializeObject(Content, SerializerSettings);

			Log.Debug("Payload - " + json);

			var response = await HttpClient.PostAsync(Host + RequestUrl, new StringContent(json, Encoding, MediaType));

			return await DeserializeResponseAsync(response);
		}
	}
}
