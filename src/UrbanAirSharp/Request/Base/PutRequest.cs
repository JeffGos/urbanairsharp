using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanAirSharp.Response;

namespace UrbanAirSharp.Request.Base
{
	public class PutRequest<TResponse, TContent> : BaseRequest<TResponse> where TResponse : BaseResponse, new()
	{
        public readonly Encoding Encoding = Encoding.UTF8;
        public const String MediaType = "application/json";

        protected TContent Content;

		public PutRequest(TContent content)
			: base(ServiceModelConfig.Host, ServiceModelConfig.HttpClient, ServiceModelConfig.SerializerSettings)
        {
			RequestMethod = HttpMethod.Put;
            Content = content;
        }

		public override async Task<TResponse> ExecuteAsync()
        {
            Log.Debug(RequestMethod + " - " + Host + RequestUrl);

            var json = JsonConvert.SerializeObject(Content, SerializerSettings);

            Log.Info("Payload - " + json);

            var response = await HttpClient.PutAsync(Host + RequestUrl, new StringContent(json, Encoding, MediaType));

            return await DeserializeResponseAsync(response);
        }
    }
}
