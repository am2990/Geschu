using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;


namespace GestureClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            List<Device> recentDevices = (List<Device>)Database.get("devices");
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

        private void Add_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddDevice.xaml", UriKind.Relative));
        }



    }
}