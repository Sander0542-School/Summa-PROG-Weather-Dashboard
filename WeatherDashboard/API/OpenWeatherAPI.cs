using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Popups;

/**
 * API KEY: 851aca63480282296a52f0a56934d673
 */
namespace WeatherDashboard.API
{
    class OpenWeatherAPI
    {
        public async Task<WeatherJSON> GetCurrent(double lat, double lng)
        {
            //Retrieve Json from server (with HTTPClient)
            using (HttpClient hcClient = new HttpClient())
            {
                // Build URL
                string sUrl = "http://api.openweathermap.org/data/2.5/weather?APPID=851aca63480282296a52f0a56934d673&lat=" + Uri.EscapeDataString(lat.ToString()) + "&lon=" + Uri.EscapeDataString(lng.ToString());

                try
                {
                    string json = await hcClient.GetStringAsync(sUrl);
                    WeatherJSON weatherJSON = JsonConvert.DeserializeObject<WeatherJSON>(json);

                    return weatherJSON;
                }
                catch (Exception ex)
                {
                    //await new MessageDialog("Er is iets fout gegaan tijdens het ophalen van het weer voor de locatie.", "Kon weer niet ophalen").ShowAsync();
                    await new MessageDialog(ex.Message, "Kon weer niet ophalen").ShowAsync();
                }
            }

            return null;
        }

        public async Task<WeatherJSON> GetCurrent(string sLocation)
        {
            //Retrieve Json from server (with HTTPClient)
            using (HttpClient hcClient = new HttpClient())
            {
                // Build URL
                string sUrl = "http://api.openweathermap.org/data/2.5/weather?APPID=851aca63480282296a52f0a56934d673&q=" + Uri.EscapeDataString(sLocation);

                try
                {
                    string json = await hcClient.GetStringAsync(sUrl);
                    WeatherJSON weatherJSON = JsonConvert.DeserializeObject<WeatherJSON>(json);

                    return weatherJSON;
                }
                catch (Exception ex)
                {
                    //await new MessageDialog("Er is iets fout gegaan tijdens het ophalen van het weer voor de locatie.", "Kon weer niet ophalen").ShowAsync();
                    await new MessageDialog("Controleer of u de plaats naam goed heeft ingevuld", "Kon weer niet ophalen").ShowAsync();
                }
            }

            return null;
        }

        public async Task<WeatherJSON> GetForecast(double lat, double lng)
        {
            //Retrieve Json from server (with HTTPClient)
            using (HttpClient hcClient = new HttpClient())
            {
                // Build URL
                string sUrl = "http://api.openweathermap.org/data/2.5/forecast?APPID=851aca63480282296a52f0a56934d673&lat=" + Uri.EscapeDataString(lat.ToString()) + "&lon=" + Uri.EscapeDataString(lng.ToString());

                try
                {
                    string json = await hcClient.GetStringAsync(sUrl);
                    WeatherJSON weatherJSON = JsonConvert.DeserializeObject<WeatherJSON>(json);

                    return weatherJSON;
                }
                catch (Exception ex)
                {
                    //await new MessageDialog("Er is iets fout gegaan tijdens het ophalen van het weer voor de locatie.", "Kon weer niet ophalen").ShowAsync();
                    await new MessageDialog(ex.Message, "Kon weer niet ophalen").ShowAsync();
                }
            }

            return null;
        }
    }
}
