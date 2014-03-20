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
            List<Device> onlineDevices = new List<Device>();
            Device device = new Device();
            device.id = 0;
            device.deviceName = "Apurv di lappy";
            //device.deviceOS = "Windows 8";
            //device.deviceType = "Computer";
            onlineDevices.Add(device);
            Device device_1 = new Device();
            device_1.id = 1;
            device_1.deviceName = "Apurv di lappy";
            //device_1.deviceOS = "Windows 8";
            //device_1.deviceType = "Computer";
            onlineDevices.Add(device_1);
            //devicesList.ItemsSource = onlineDevices;
            devicesList.ItemsSource = Device.getAll();


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