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
    public partial class Profile_VLC : PhoneApplicationPage
    {
        private string serverName = "192.168.48.21";
        private int portNumber = 12424;
        private string data;

        Connections sendData = new Connections();

        public Profile_VLC()
        {
            InitializeComponent();
        }

        private void Play_Down(object sender, System.Windows.Input.KeyEventArgs e)
        {
            data = "1:32";
            sendData.Send(serverName, portNumber, data);
        }

        private void Play_Up(object sender, System.Windows.Input.KeyEventArgs e)
        {
            data = "1:32";
            sendData.Send(serverName, portNumber, data);
        }

    }
}