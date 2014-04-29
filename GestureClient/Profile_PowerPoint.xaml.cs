using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;

namespace GestureClient
{
    public partial class Profile_PowerPoint : PhoneApplicationPage
    {
        private string serverName = "192.168.48.21"; // getServerName();
        private int portNumber = 12424; // getPortNumber();
        private string data;
        private string deviceid;

        Connections conn = new Connections();

        public Profile_PowerPoint()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeComponent();
            string id;
            if (NavigationContext.QueryString.TryGetValue("Id", out id))
            {
                deviceid = id;
                List<Device> devices = (List<Device>)Database.Get("device");
                foreach (Device d in devices)
                {
                    if ((d.id.ToString()) == id)
                    {
                        serverName = d.deviceIP;
                        break;
                    }
                }


            }
            else
            {
                //show user an error messgae
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            String uri = "/userProfiles.xaml?" + "Id=" + deviceid;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var content = button.Tag;
            data = "1:" + content;
            int c = 0;
            while (c < 10)
            {
                System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                conn.Send(serverName, portNumber, data);
            }

        }

        private void OnMouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var content = button.Tag;
            data = "0:" + content;
            int c = 0;
            while (c < 10)
            {
                System.Diagnostics.Debug.WriteLine("Fired UP {0}", c++);
                conn.Send(serverName, portNumber, data);
            }
        }
    }
}