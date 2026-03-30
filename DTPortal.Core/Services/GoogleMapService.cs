using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using DTPortal.Core.Domain.Services;

namespace DTPortal.Core.Services
{
    public class AddressComponents
    {
        public string long_name { get; set; }

        public string short_name { get; set; }

        public string[] types { get; set; }
    }

    public class GoogleMapService : IGoogleMapService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GoogleMapService> _logger;

        public GoogleMapService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<GoogleMapService> logger)
        {
            _configuration = configuration;
            httpClient.BaseAddress = new Uri(_configuration["APIServiceLocations:GoogleMapBaseAddress"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _client = httpClient;
            _logger = logger;
        }

        public async Task<string> GetAddressByLatitudeLongitude(string latitude, string longitude)
        {
            //string[] locations = new string[] { "locality", "administrative_area_level_2", "administrative_area_level_1", "country" };
            string[] locations = new string[] { "administrative_area_level_1", "country" };
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"maps/api/geocode/json?latlng={latitude},{longitude}&key={_configuration["GoogleMapAPIKey"]}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var apiResponse = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    JToken result = apiResponse["results"][1];
                    var addressComponentsList = JsonConvert.DeserializeObject<IEnumerable<AddressComponents>>(result["address_components"].ToString());
                    List<string> addressArray = new List<string>();
                    foreach (var address in addressComponentsList)
                    {
                        foreach (var location in locations)
                        {
                            if (address.types.Any(x => x == location))
                            {
                                addressArray.Add(address.long_name);
                            }
                        }
                    }

                    return string.Join(", ", addressArray);
                }
                else
                {
                    _logger.LogError($"The request with URI={response.RequestMessage.RequestUri} failed " +
                         $"with status code={response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }
    }
}
