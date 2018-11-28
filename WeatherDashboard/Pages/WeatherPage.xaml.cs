using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WeatherDashboard.Common;
using WeatherDashboard.API;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherDashboard.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeatherPage : Page
    {
        private NavigationHelper _navigationHelper;
        private ApplicationDataContainer _adclocalSettings = ApplicationData.Current.LocalSettings;
        
        public WeatherPage()
        {
            this.InitializeComponent();

            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += _navigationHelper_LoadState;
            _navigationHelper.SaveState += _navigationHelper_SaveState;

            LoadWeather();
        }

        public async void LoadWeather()
        {
            OpenWeatherAPI openWeatherAPI = new OpenWeatherAPI();
            
            if (_adclocalSettings.Values.ContainsKey("lat") && _adclocalSettings.Values.ContainsKey("lng")) {

                double.TryParse(_adclocalSettings.Values["lat"].ToString(), out double lat);
                double.TryParse(_adclocalSettings.Values["lng"].ToString(), out double lng);

                WeatherJSON weather = await openWeatherAPI.GetCurrent(lat, lng);

                tbLocatie.Text = weather.Name;
                tbTemperatuur.Text = KToC(weather.Main.Temp).ToString() + "°C";
                tbMinTemperatuur.Text =  KToC(weather.Main.TempMin) + "°C";
                tbMaxTemperatuur.Text =  KToC(weather.Main.TempMax) + "°C";
                tbLuchtvochtigheid.Text = weather.Main.Humidity.ToString() + "%";
                tbWindSnelheid.Text = weather.Wind.Speed.ToString() + " km/h";
                tbWindRichting.Text = Richting(weather.Wind.Deg);
                //tbOmschrijving.Document.SetText(Windows.UI.Text.TextSetOptions.None, weather.Weather.Description.ToString());

            }
        }

        public String Richting(String sDeg)
        {
            try {
                Double dDeg = Convert.ToDouble(sDeg);
                string[] sRichting = { "Noord", "Noord-noord-oost", "Noord-oost", "Oost-noord-oost", "Oost", "Oost-zuid-oost", "Zuid-oost", "Zuid-zuid-oost", "Zuid", "Zuid-zuid-west", "Zuid-west", "West-zuid-west", "West", "West-noord-west", "Noord-West", "Noord-noord-west", "Noord" };
                return sRichting[(int)Math.Round(((double)dDeg * 10 % 3600) / 225)];
            }
            catch {
                return "Geen richting gevonden";
            }
                

        }

        public String KToC(Double dTemp)
        {
            try
            {
                string sTemp;
                dTemp = Math.Round(dTemp - 273.15);
                return sTemp = dTemp.ToString();
            }
            catch
            {
                return "Geen info";
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        private void _navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }

        private void _navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }
    }
}
