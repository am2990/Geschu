using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GestureClient
{
    public partial class AddDevice : PhoneApplicationPage
    {
        private List<Device> onlineDevices = null;

        public AddDevice()
        {
            InitializeComponent();
        }

        private void DevicesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Device item = (Device) devicesList.SelectedItem;
            String uri = "/UserProfile.xaml?" + "Id=" + "1";
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void findOnlineDevices()
        {
            onlineDevices = Device.detect();
            devicesList.ItemsSource = onlineDevices;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            //this.findOnlineDevices();
            List<Device> NewDevices = new List<Device>();
            Device Device = new Device();
            Device.deviceType = "";
            Device.deviceOS = "Android";
            Device.deviceName = "Pendru";
            Device.deviceIP = "";
            Device.id = 3;
            NewDevices.Add(Device);
            devicesList.ItemsSource = NewDevices;
        }
    }
}