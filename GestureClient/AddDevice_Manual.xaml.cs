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
    public partial class AddDevice_Manual : PhoneApplicationPage
    {
        public AddDevice_Manual()
        {
            InitializeComponent();
        }


        private void Save_Device(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(DeviceName.Text))
            {
                MessageBox.Show("A Device Name is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(DeviceIP.Text))
            {
                MessageBox.Show("The Device IP Address is required.");
                return;
            }

            else
            {
                Device saveDevice = new Device(DeviceName.Text.GetHashCode(), DeviceName.Text, DeviceIP.Text);
                List<Device> deviceList = (List<Device>)Database.get("device");
                
                if (!deviceList.Contains(saveDevice))
                {
                    if (deviceList.Count > 3)
                    {
                        deviceList.RemoveAt(3);
                    }
                    deviceList.Add(saveDevice);
                    Database.add("device", deviceList);
                }

                NavigationService.Navigate(new Uri("/Mainpage.xaml", UriKind.Relative));
            }



        }

        private void Remove_Text(object sender, RoutedEventArgs e)
        {
            TextBox t = sender as TextBox;
            t.Text = "";
        }
    }
}