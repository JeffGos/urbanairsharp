using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UrbanAirSharp
{
	public static class ServiceModelConfig
	{
		public static readonly String Host = "https://go.urbanairship.com/";
		public static readonly HttpClient HttpClient = new HttpClient();
		public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings();

		public static void Create(String uaAppKey, String uaAppMAsterSecret)
		{
			var auth = String.Format("{0}:{1}", uaAppKey, uaAppMAsterSecret);
			auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));

			SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
			SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
			HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/vnd.urbanairship+json; version=3;");
		}
	}
}
