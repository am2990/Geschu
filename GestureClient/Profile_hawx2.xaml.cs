using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace GestureClient
{
    public partial class Profile_hawx2 : PhoneApplicationPage
    {
        Motion motion;
        
        private string serverName = "";
        private int portNumber = 12424;
        private string data;
        private string deviceid;

        private bool setval = false;
        float _roll = 0;
        float _pitch = 0;
        float _yaw = 0;

        Connections sendData = new Connections();


        public Profile_hawx2()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("Id", out deviceid))
            {
                List<Device> devices = (List<Device>)Database.get("device");
                foreach (Device d in devices)
                {
                    if ((d.id.ToString()) == deviceid)
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

            // Check to see if the Motion API is supported on the device.
            if (!Motion.IsSupported)
            {
                MessageBox.Show("the Motion API is not supported on this device.");
                return;
            }

           
            // If the Motion object is null, initialize it and add a CurrentValueChanged
            // event handler.
            if (motion == null)
            {
                motion = new Motion();
                // motion.TimeBetweenUpdates = TimeSpan.FromMilliseconds(0);
                motion.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<MotionReading>>(motion_CurrentValueChanged);
            }

            // Try to start the Motion API.
            try
            {
                motion.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("unable to start the Motion API.");
            }
            this.DisableLocking();
        }

        private void DisableLocking()
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            motion.Stop();
            String uri = "/UserProfile.xaml?" + "Id=" + deviceid;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        void motion_CurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            // This event arrives on a background thread. Use BeginInvoke to call
            // CurrentValueChanged on the UI thread.
            CurrentValueChanged(e.SensorReading);
            //Dispatcher.BeginInvoke(() => CurrentValueChanged(e.SensorReading));
        }
        private void CurrentValueChanged(MotionReading e)
        {
            if (motion.IsDataValid)
            {

                //string text = MathHelper.ToDegrees(e.Attitude.Roll).ToString("0") + ":" +
                  //            MathHelper.ToDegrees(e.Attitude.Pitch).ToString("0") + ":" +
                    //          MathHelper.ToDegrees(e.Attitude.Yaw).ToString("0");
                 
                
                if (!setval)
                {
                    _pitch = MathHelper.ToDegrees(e.Attitude.Pitch);
                    _roll = MathHelper.ToDegrees(e.Attitude.Roll); 
                    //_roll = -30;
                    _yaw = MathHelper.ToDegrees(e.Attitude.Yaw);

                    setval = true;
                }

                float delta_pitch = MathHelper.ToDegrees(e.Attitude.Pitch) - _pitch;
                float delta_roll = MathHelper.ToDegrees(e.Attitude.Roll) - _roll;
                float delta_yaw = MathHelper.ToDegrees(e.Attitude.Yaw) - _yaw;

                string text = delta_pitch.ToString("0") + ":" +
                              delta_roll.ToString("0") + ":" +
                              delta_yaw.ToString("0");
                /*
                //processValues(MathHelper.ToDegrees(e.Attitude.Pitch), MathHelper.ToDegrees(e.Attitude.Roll), MathHelper.ToDegrees(e.Attitude.Yaw));
                //processRollValues(MathHelper.ToDegrees(e.Attitude.Roll));
                //processPitchValues(MathHelper.ToDegrees(e.Attitude.Pitch));
                //processRollValues(MathHelper.ToDegrees(e.Attitude.Yaw));
                // Attempt to send our message to be echoed to the echo server
                */
                sendData.Send(serverName, portNumber, text);

            }
        }

        private void processPitchValues(float pitch)
        {
            float delta_pitch = pitch - _pitch;
            string keydata = "0:0";

            if (delta_pitch < -25)
            {
                //left arrow
                keydata = "2:37";

                int c = 0;
                System.Diagnostics.Debug.WriteLine("Left Arrow {0}", delta_pitch);
               // while (c < 10)
               // {
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, keydata);
                    c++;
              //  }
            }
            else if (delta_pitch > 25)
            {
                //right arrow
                keydata = "2:39";

                int c = 0;
                System.Diagnostics.Debug.WriteLine("Right Arrow {0}", delta_pitch);
                //while (c < 10)
                //{
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, keydata);
                    c++;
                //}
            }
            else
            {
                string key = keydata.Split(':')[1];

                keydata = "0:"+key;

                int c = 0;
                System.Diagnostics.Debug.WriteLine("Pitch Up {0}", delta_pitch);
                //while (c < 10)
                //{
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, keydata);
                    //c++;
                //}
            }
        }

        private void processRollValues(float roll)
        {
                        
            float delta_roll = roll - _roll;
            string keydata = "0:0";

            if (delta_roll < -15)
            {
                //down arrow
                keydata = "2:40";
            
                int c = 0;
                System.Diagnostics.Debug.WriteLine("Down Arrow {0}", delta_roll);
                //while (c < 10)
                //{
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, keydata);
                    c++;
                //}
            }
            else if (delta_roll > 15)
            {
                //up arrow
                keydata = "2:38";

                int c = 0;
                System.Diagnostics.Debug.WriteLine("Up Arrow {0}", delta_roll);
                //while (c < 10)
                //{
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, keydata);
                    c++;
               // }
            }
            else
            {
                string key = keydata.Split(':')[1];

                keydata = "0:" + key;

                int c = 0;
                System.Diagnostics.Debug.WriteLine("Roll Up {0}", delta_roll);
                //while (c < 10)
                //{
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                     sendData.Send(serverName, portNumber, keydata);
                    //c++;
               // }
            }

        }

        private void processValues(float pitch, float roll, float yaw)
        {
            float delta_pitch = pitch - _pitch;
            float delta_roll = roll - _roll;
            float delta_yaw = yaw - _yaw;
            string prev = "0";

            if (delta_pitch > 30)
            {
                //right arrow
                data = "1:" + "39";
                prev = "39";
                int c = 0;
                System.Diagnostics.Debug.WriteLine("Right Arrow {0}", delta_pitch);

                //while (c < 10)
               // {
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, data);
                    c++;
               // }
            }
            else if (delta_pitch < -30)
            {
                //left arrow
                data = "1:37";
                prev = "37";
                int c = 0;
                System.Diagnostics.Debug.WriteLine("Left Arrow {0}", delta_pitch);
                while (c < 10)
                {
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, data);
                    c++;
                }
            }
            else if (delta_roll < -30)
            {
                //down arrow
                data = "1:40";
                prev = "40";
                int c = 0;
                System.Diagnostics.Debug.WriteLine("Down Arrow {0}", delta_roll);
                while (c < 10)
                {
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, data);
                    c++;
                }
            }
            else if (delta_roll > 30)
            {
                //up arrow
                data = "1:38";
                prev = "38";
                int c = 0;
                System.Diagnostics.Debug.WriteLine("Up Arrow {0}", delta_roll);
                while (c < 10)
                {
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, data);
                    c++;
                }
            }
            else if (delta_yaw < -20)
            {
                //E arrow
                data = "1:69";
                prev = "69";
                int c = 0;
                System.Diagnostics.Debug.WriteLine("E {0}", delta_yaw);
                while (c < 10)
                {
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, data);
                    c++;
                }
            }
            else if (delta_yaw > 20)
            {
                //Q arrow
                data = "1:81";
                prev = "81";
                int c = 0;
                System.Diagnostics.Debug.WriteLine("Q {0}", delta_yaw);
                while (c < 10)
                {
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, data);
                    c++;
                }
            }
            else
            {
                data = "0:" + prev;
                int c = 0;
                System.Diagnostics.Debug.WriteLine("KeyUp {0}", data);
                while (c < 10)
                {
                    //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                    //data = "1:32";
                    sendData.Send(serverName, portNumber, data);
                    c++;
                }
            }

            
        }

        private void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var content = button.Tag;
            data = "1:" + content;
            int c = 0;
            System.Diagnostics.Debug.WriteLine("MouseDown {0}", data);
            while (c < 10)
            {
                //System.Diagnostics.Debug.WriteLine("Fired down {0}", c++);
                //data = "1:32";
                sendData.Send(serverName, portNumber, data);
                c++;
            }
        }

        private void OnMouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            var content = button.Tag;
            data = "0:" + content;
            int c = 0;
            System.Diagnostics.Debug.WriteLine("MouseUp {0}", data);
            while (c < 10)
            {
                //System.Diagnostics.Debug.WriteLine("Fired UP {0}", c++);
                //data = "0:32";
                sendData.Send(serverName, portNumber, data);
                c++;
            }
        }
        
    }
}