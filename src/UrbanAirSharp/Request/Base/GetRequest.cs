// Copyright (c) 2014-2015 Jeff Gosling (jeffery.gosling@gmail.com)

using System.Net.Http;
using System.Threading.Tasks;
using UrbanAirSharp.Response;

namespace UrbanAirSharp.Request.Base
{
	/// <summary>
	/// Used to form a VALIDATE request
	/// Accept the same range of payloads as /api/push, but parse and validate only, without sending any pushes
	/// 
	/// http://docs.urbanairship.com/reference/api/v3/push.html
	/// </summary>
	public class GetRequest<TResponse> : BaseRequest<TResponse> where TResponse : BaseResponse, new()
	{
		public GetRequest()
			: base(ServiceModelConfig.Host, ServiceModelConfig.HttpClient, ServiceModelConfig.SerializerSettings)
		{
			RequestMethod = HttpMethod.Get;
		}

		public override async Task<TResponse> ExecuteAsync()
		{
			Log.Debug(RequestMethod + " - " + Host + RequestUrl);

			var response = await HttpClient.GetAsync(Host + RequestUrl);

			return await DeserializeResponseAsync(response);
		}
	}
}
