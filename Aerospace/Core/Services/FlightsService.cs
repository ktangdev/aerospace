using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FlightsService : IFlightsService
    {
        readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Get flights returns a list of all current flights
        /// </summary>
        /// <returns></returns>
        public string GetFlights()
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync("https://opensky-network.org/api/states/all").Result;

                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        var result = content.ReadAsStringAsync();

                        return result.Result;
                    }
                }

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
