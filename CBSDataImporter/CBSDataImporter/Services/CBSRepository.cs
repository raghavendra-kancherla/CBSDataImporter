using CBSDataImporter.Models;
using CBSDataImporter.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace CBSDataImporter.Services
{
    public class CBSRepository : ICBSRepository
    {
        private readonly IConfiguration configuration;
        public CBSRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Player> GetPlayers(string sportName)
        {
            var result = new List<Player>();
            var url = BuildUrlPerSport(sportName);

            result = GetPlayerData(url)?.body?.players;

            return result;
        }

        private string BuildUrlPerSport(string sportName)
        {
            var url = configuration["CBS:URL"];
            var responseFormat = configuration["CBS:ResponseFormat"];
            return url + sportName.ToLower() + responseFormat;
        }

        private CBSResponse GetPlayerData(string url)
        {
            var result = new CBSResponse();
            var t = Task.Run(() => HttpHelper.GetData(url));
            t.Wait();
            result = JsonConvert.DeserializeObject<CBSResponse>(t.Result);


            return result;
        }
    }
}
