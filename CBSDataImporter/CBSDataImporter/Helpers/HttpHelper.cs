using System.Net.Http.Headers;
using System.Net;
using System.Text;
using CBSDataImporter.Models;
using Newtonsoft.Json;

public static class HttpHelper
{
	public static async Task<string> GetData(string url)
	{
		string result =  string.Empty;

		using (HttpClient client = new HttpClient())
		{
			try
			{
				HttpResponseMessage response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					result = await response.Content.ReadAsStringAsync();
				}
			}
			catch (HttpRequestException e)
			{
			 //Log errors into the logging system
			}
		}
		return result;
	}
}


