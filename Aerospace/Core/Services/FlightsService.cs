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
        /// The following API call can be used to retrieve any state vector of the OpenSky.
        /// The states property is a two-dimensional array. Each row represents a state vector.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AircraftState> GetStates()
        {
            List<AircraftState> aircraftStates = new List<AircraftState>();

            try
            {
                // Sends a request to the API endpoint
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync("https://opensky-network.org/api/states/all").Result;

                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        // Upon a successful request read JSON object
                        var result = content.ReadAsStringAsync();
                        var jsonObj = JObject.Parse(result.Result);
                        var jsonStates = jsonObj["states"].ToList();

                        // Loop through each aircraft state and add it to a list
                        foreach (var state in jsonStates)
                        {
                            AircraftState aircraftState = new AircraftState
                            {
                                Icao24 = state.First.ToString() ?? "",
                                CallSign = state[1].ToString() ?? "",
                                OriginCountry = state[2].ToString() ?? "",
                                TimePosition = state[3].ToObject<int?>(),
                                LastContact = state[4].ToObject<int?>(),
                                Longitude = state[5].ToObject<float?>(),
                                Latitude = state[6].ToObject<float?>(),
                                BaroAltitude = state[7].ToObject<float?>(),
                                OnGround = state[8].ToObject<bool?>(),
                                Velocity = state[9].ToObject<float?>(),
                                TrueTrack = state[10].ToObject<float?>(),
                                VerticalRate = state[11].ToObject<float?>(),
                                Sensors = state[12].ToObject<int?[]>(),
                                GeoAltitude = state[13].ToObject<float?>(),
                                Squawk = state[14].ToString() ?? "",
                                Spi = state[15].ToObject<bool?>(),
                                PositionSource = state.Last.ToObject<int?>()
                            };

                            aircraftStates.Add(aircraftState);
                        }

                        return aircraftStates;
                    }
                }
                else
                {
                    // When response code is not successful, return empty list
                    return aircraftStates;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return aircraftStates;
            }
        }

        /// <summary>
        /// This API call retrieves flights for a certain time interval [begin, end].
        /// If no flights are found for the given time period, HTTP status 404 - Not found is returned with an empty response body.
        /// The response is a JSON array of flights where each flight is an object.
        /// </summary>
        /// <param name="beginTime">Start of time interval to retrieve flights for as Unix time (seconds since epoch)</param>
        /// <param name="endTime">End of time interval to retrieve flights for as Unix time (seconds since epoch)</param>
        /// <returns></returns>
        public IEnumerable<Flight> GetFlightsByTime(int beginTime, int endTime)
        {
            List<Flight> flights = new List<Flight>();
            try
            {
                // Sends a request to the API endpoint
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync("https://opensky-network.org/api/flights/all?begin=" + beginTime + "&end=" + endTime).Result;

                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        // Upon a successful request read JSON object
                        var result = content.ReadAsStringAsync();

                        // Deserialize Json array straight into List of Flight objects
                        flights = JsonConvert.DeserializeObject<List<Flight>>(result.Result);

                        return flights;
                    }
                }
                else
                {
                    // When response code is not successful, return empty list
                    return flights;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return flights;
            }
        }

        /// <summary>
        /// This API call retrieves flights for a particular aircraft within a certain time interval. Resulting flights departed and arrived within [begin, end].
        /// If no flights are found for the given period, HTTP stats 404 - Not found is returned with an empty response body.
        /// The response is a JSON array of flights where each flight is an object.
        /// </summary>
        /// <param name="icao">Unique ICAO 24-bit address of the transponder in hex string representation. All letters need to be lower case</param>
        /// <param name="beginTime">Start of time interval to retrieve flights for as Unix time (seconds since epoch)</param>
        /// <param name="endTime">End of time interval to retrieve flights for as Unix time (seconds since epoch)</param>
        /// <returns></returns>
        public IEnumerable<Flight> GetFlightsByAircraft(string icao, int beginTime, int endTime)
        {
            List<Flight> flights = new List<Flight>();
            try
            {
                // Sends a request to the API endpoint
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync("https://opensky-network.org/api/flights/aircraft?icao24=" + icao.ToLower() + "&begin=" + beginTime + " &end=" + endTime).Result;

                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        // Upon a successful request read JSON object
                        var result = content.ReadAsStringAsync();

                        // Deserialize Json array straight into List of Flight objects
                        flights = JsonConvert.DeserializeObject<List<Flight>>(result.Result);

                        return flights;
                    }
                }
                else
                {
                    // When response code is not successful, return empty list
                    return flights;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return flights;
            }
        }


        /// <summary>
        /// Retrieve flights for a certain airport which arrived within a given time interval [begin, end].
        /// If no flights are found for the given period, HTTP stats 404 - Not found is returned with an empty response body.
        /// The response is a JSON array of flights where each flight is an object.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Flight> GetArrivalsByAirport()
        {
            List<Flight> flights = new List<Flight>();
            try
            {
                return flights;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return flights;
            }
        }

        /// <summary>
        /// Retrieve flights for a certain airport which departed within a given time interval [begin, end].
        /// If no flights are found for the given period, HTTP stats 404 - Not found is returned with an empty response body.
        /// The response is a JSON array of flights where each flight is an object.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Flight> GetDeparturesByAirport()
        {
            List<Flight> flights = new List<Flight>();
            try
            {
                return flights;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return flights;
            }
        }
    }
}
