using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;


namespace GestureClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            InitializeComponent();
            this.LoadData();
        }
        
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                while (NavigationService.RemoveBackEntry() != null)
                {
                    NavigationService.RemoveBackEntry();
                }
            }
        }

        private void LoadData()
        {
            List<Device> recentDevices = (List<Device>)Database.Get("device");
            devicesList.ItemsSource = recentDevices;


        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void AddClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddDevice.xaml", UriKind.Relative));
        }

        private void ClearDevices(object sender, EventArgs e)
        {
            Database.Remove("device");
            NavigationService.Navigate(new Uri(string.Format(NavigationService.Source +
                                    "?Refresh=true&random={0}", Guid.NewGuid()), UriKind.Relative));        
        }

        private void DevicesListSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Device item = (Device)devicesList.SelectedItem;
            String uri = "/userProfiles.xaml?" + "Id=" + item.id;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }



    }
}