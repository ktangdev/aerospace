using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                        List<AircraftState> aircraftStates = new List<AircraftState>();

                        var result = content.ReadAsStringAsync();

                        var test = JsonConvert.DeserializeObject<AircraftStates>(result.Result);

                        var obj = JObject.Parse(result.Result);
                        var states = obj["states"].ToList();

                        foreach (var state in states)
                        {
                            string Icao24 = state.First.ToString() ?? "";
                            string CallSign = state[1].ToString() ?? "";
                            string OriginCountry = state[2].ToString() ?? "";
                            int TimePosition = state[3].ToObject<int>();
                            int LastContact = state[4].ToObject<int>();
                            float Longitude = state[5].ToObject<float>();
                            float Latitude = state[6].ToObject<float>();
                            float GeoAltitude = state[7].ToObject<float>();
                            bool OnGround = state[8].ToObject<bool>();
                            float Velocity = state[9].ToObject<float>();
                            float Heading = state[10].ToObject<float>();
                            float VerticalRate = state[11].ToObject<float>();
                            int[] Sensors = state[12].ToObject<int[]>();
                            float BaroAltitude = state[13].ToObject<float>();
                            string Squawk = state[14].ToString();
                            bool Spi = state[15].ToObject<bool>();
                            int PositionSource = state.Last.ToObject<int>();

                            //AircraftState aircraftState = new AircraftState
                            //{
                            //    Icao24 = state.First.ToString(),
                            //    CallSign = state[1].ToString(),
                            //    OriginCountry = state[2].ToString(),
                            //    TimePosition = state[3].ToObject<int>(),
                            //    LastContact = state[4].ToObject<int>(),
                            //    Longitude = state[5].ToObject<float>(),
                            //    Latitude = state[6].ToObject<float>(),
                            //    GeoAltitude = state[7].ToObject<float>(),
                            //    OnGround = state[8].ToObject<bool>(),
                            //    Velocity = state[9].ToObject<float>(),
                            //    Heading = state[10].ToObject<float>(),
                            //    VerticalRate = state[11].ToObject<float>(),
                            //    Sensors = state[12].ToObject<int[]>(),
                            //    BaroAltitude = state[13].ToObject<float>(),
                            //    Squawk = state[14].ToString(),
                            //    Spi = state[15].ToObject<bool>(),
                            //    PositionSource = state.Last.ToObject<int>()
                            //};

                            //aircraftStates.Add(aircraftState);
                        }

                        return aircraftStates.Count().ToString();
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
