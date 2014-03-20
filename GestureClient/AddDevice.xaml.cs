using Microsoft.Phone.Controls;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Diagnostics;

namespace GestureClient
{
    public partial class AddDevice : PhoneApplicationPage
    {
        private List<Device> onlineDevices = null;
        private BackgroundWorker bw = new BackgroundWorker();

        

        public AddDevice()
        {
            InitializeComponent();

            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.findOnlineDevices();
        }


        private void DevicesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Device item = (Device)devicesList.SelectedItem;
            String uri = "/UserProfile.xaml?" + "Id=" + item.id;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void findOnlineDevices()
        {
            
            Debug.WriteLine("Searching for Online Devices");
            
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
            
            System.Threading.Thread.Sleep(100);

            devicesList.ItemsSource = onlineDevices;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            this.findOnlineDevices();
            List<Device> NewDevices = new List<Device>();
            //Device Device = new Device();
            //Device.deviceType = "";
            //Device.deviceOS = "Android";
            //Device.deviceName = "Pendru";
            //Device.deviceIP = "";
            //Device.id = 3;
            //NewDevices.Add(Device);
            //devicesList.ItemsSource = NewDevices;
            devicesList.ItemsSource = onlineDevices;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Connections discovery = new Connections();
            onlineDevices = new List<Device>();
            Debug.WriteLine("background Task Started");

            for (int i = 1; i <= 1; i++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    //string device = TcpSocket.Receive();
                                    discovery.SendDiscovery();
                    string device = discovery.Receive();

                    if (device.Contains("Operation Timeout"))
                    {
                        continue;
                    }
                    string[] temp = device.Split(':');
                    Device d = new Device();
                    d.deviceName = temp[0];
                    d.deviceIP = temp[1];
                    d.devicePort = temp[2];
                    d.id = i;
                   // if (!onlineDevices.Contains(d))
                    //{
                        onlineDevices.Add(d);
                    //}
                    worker.ReportProgress(i * 10);
                }
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
            devicesList.ItemsSource = onlineDevices;

        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.tbProgress.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                this.tbProgress.Text = ("Error: " + e.Error.Message);
                
            }

            else
            {
                this.tbProgress.Text = "Done!";
            }
        }
    }
}