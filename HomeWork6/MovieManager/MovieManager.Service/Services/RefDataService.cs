using Microsoft.Extensions.Configuration;
using MovieManager.Service.Common;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace MovieManager.Service.Services
{
	public interface IRefDataService
	{
        Task<IEnumerable<T>> GetData<T>(string path);

		Task<T> PostData<T>(string path, T data);

    }

	public class RefDataService : HttpClientBase, IRefDataService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public RefDataService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_httpClient = httpClientFactory.CreateClient();
			_configuration = configuration;
		}

		public async Task<IEnumerable<T>> GetData<T>(string path)
		{
			var endpoint = _configuration.GetSection($"RefDataEndpoints:{path}").Value;
            return (await Get<List<T>>(_httpClient, new Uri(endpoint))).Value;
		}

		public async Task<T> PostData<T>(string path, T data)
		{
			var endpoint = _configuration.GetSection($"RefDataEndpoints:{path}").Value;
			var body = JsonConvert.SerializeObject(data);
			return (await Post<T>(_httpClient, new Uri(endpoint), body)).Value;
		}
    }
}
