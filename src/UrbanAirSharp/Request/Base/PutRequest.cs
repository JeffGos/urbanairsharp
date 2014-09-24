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
    public class PutRequest<T> : BaseRequest
    {
        public readonly Encoding Encoding = Encoding.UTF8;
        public const String MediaType = "application/json";

        protected T Content;


        public PutRequest(T content) : base(ServiceModelConfig.Host, ServiceModelConfig.HttpClient, ServiceModelConfig.SerializerSettings)
        {
            Content = content;
        }

        public override async Task<BaseResponse> ExecuteAsync()
        {
            Log.Debug(RequestMethod + " - " + Host + RequestUrl);

            var json = JsonConvert.SerializeObject(Content, SerializerSettings);

            Log.Debug("Payload - " + json);

            var response = await HttpClient.PutAsync(Host + RequestUrl, new StringContent(json, Encoding, MediaType));

            return await DeserializeResponseAsync(response);
        }
    }
}
