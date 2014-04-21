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
    public partial class Profile_VLC : PhoneApplicationPage
    {
        private string serverName = "";
        private int portNumber = 12424;
        private string data;
        private string deviceid;

        Connections sendData = new Connections();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeComponent();
            string id;
            if (NavigationContext.QueryString.TryGetValue("Id", out id))
            {
                deviceid = id;
                List<Device> devices = (List<Device>)Database.get("device");
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


        public Profile_VLC()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            String uri = "/UserProfile.xaml?" + "Id=" + deviceid;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var content = button.Tag;
            data = "1:" + content;
            int c = 0;
            while ( c < 10 )
            {
                System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                //data = "1:32";
                sendData.Send(serverName, portNumber, data);
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
                //data = "0:32";
                sendData.Send(serverName, portNumber, data);
            }
        }

        private void OnJumpDown(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var content = button.Tag;
            data = (100+DateTime.Now.Millisecond).ToString() + ":" + content;
            int c = 0;
            while (c < 10)
            {
                System.Diagnostics.Debug.WriteLine("Fired UP {0}", c++);
                //data = "0:32";
                sendData.Send(serverName, portNumber, data);
            }
        }

    }
}