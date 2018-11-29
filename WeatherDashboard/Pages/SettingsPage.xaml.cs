using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
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
using Windows.UI.Core;
using Windows.System;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherDashboard.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private NavigationHelper _navigationHelper;
        private ApplicationDataContainer _adclocalSettings = ApplicationData.Current.LocalSettings;

        public SettingsPage()
        {
            this.InitializeComponent();

            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += _navigationHelper_LoadState;
            _navigationHelper.SaveState += _navigationHelper_SaveState;
        }



        private void tbInput_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                _adclocalSettings.Values["City"] = tbLocation.Text;
            }
        }

        private void btnCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            _adclocalSettings.Values["City"] = null;
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