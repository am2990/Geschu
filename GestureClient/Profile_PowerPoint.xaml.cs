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

        Connections conn = new Connections();

        public Profile_PowerPoint()
        {
            InitializeComponent();
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