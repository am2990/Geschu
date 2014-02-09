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
        public AddDevice()
        {
            InitializeComponent();
        }

        private void DevicesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Devices item = (Devices) DevicesList.SelectedItem;
            NavigationService.Navigate(new Uri("/Profile.xaml?Id=0", UriKind.Relative));
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            List<Devices> NewDevices = new List<Devices>();
            Devices Device = new Devices();
            Device.DeviceType = "";
            Device.DeviceOS = "Android";
            Device.DeviceName = "Pendru";
            Device.DeviceIP = "";
            Device.Id = 3;
            NewDevices.Add(Device);
            DevicesList.ItemsSource = NewDevices;
        }
    }
}