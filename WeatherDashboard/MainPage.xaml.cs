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
using WeatherDashboard.Pages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherDashboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Dictionary<string, Type> _dstPages;

        public MainPage()
        {
            this.InitializeComponent();

            _dstPages = new Dictionary<string, Type>()
            {
                { "Nav_Home", typeof(Pages.HomePage) },
                { "Nav_Settings", typeof(Pages.SettingsPage) }
            };
        }

        private void nvNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string sSelectedItem;

            if (args.IsSettingsInvoked)
            {
                sSelectedItem = "Nav_Settings";
            }
            else
            {
                sSelectedItem = (args.InvokedItem as TextBlock)?.Tag as string;
            }

            fRootFrame.Navigate(_dstPages[sSelectedItem]);
        }

        private void fRootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            string sTag = _dstPages.FirstOrDefault(x => x.Value == e.SourcePageType).Key;

            if (sTag == "Nav_Settings")
            {
                NavigationViewItemBase nvibSettings = nvNavigation.SettingsItem as NavigationViewItemBase;

                nvNavigation.SelectedItem = nvibSettings;
                nvNavigation.Header = nvibSettings.Content;
            }
            else
            {
                foreach (object oItem in nvNavigation.MenuItems)
                {
                    NavigationViewItem nviItem = oItem as NavigationViewItem;
                    TextBlock tbItem = nviItem?.Content as TextBlock;

                    if (tbItem != null && (string)tbItem.Tag == sTag)
                    {
                        nvNavigation.SelectedItem = nviItem;
                        nvNavigation.Header = tbItem.Text;
                        break;
                    }
                }
            }
        }

        private void fRootFrame_Loaded(object sender, RoutedEventArgs e)
        {
            fRootFrame.Navigate(_dstPages["Nav_Home"]);
        }
    }
}
