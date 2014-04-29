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
        private BackgroundWorker bgWorker = new BackgroundWorker();

        public AddDevice()
        {
            InitializeComponent();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += new DoWorkEventHandler(bw_DoWork);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(BWorkerProgressChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWorkerRunWorkerCompleted);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.FindOnlineDevices();
        }


        private void DevicesListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Device item = (Device)devicesList.SelectedItem;
            AddToRecentDevices(item);
            String uri = "/userProfiles.xaml?" + "Id=" + item.id;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void AddToRecentDevices(Device item)
        {
            List<Device> deviceList = Device.GetSavedDevices();
            if (!deviceList.Contains(item))
            {
                if (deviceList.Count > 3)
                {
                    deviceList.RemoveAt(3);
                }
                deviceList.Add(item);
                Database.Add("device", deviceList);
            }
        }

        private void FindOnlineDevices()
        {
            Debug.WriteLine("Searching for Online Devices");
            if (bgWorker.IsBusy != true)
            {
                bgWorker.RunWorkerAsync();
            }
            System.Threading.Thread.Sleep(100);
            devicesList.ItemsSource = onlineDevices;
        }

        private void RefreshClick(object sender, EventArgs e)
        {
            List<Device> newDevices = new List<Device>();
            Device device = new Device();
            this.FindOnlineDevices();
            device.deviceName = "Apurv PC";
            device.deviceIP = "192.168.48.21";
            device.id = 3;
            newDevices.Add(device);
            devicesList.ItemsSource = newDevices;
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
                    discovery.SendDiscovery();
                    string device = discovery.Receive();
                    if (device.Contains("Operation Timeout"))
                    {
                        continue;
                    }
                    string[] temp = device.Split(':');
                    Device d = new Device();
                    string name = temp[0];
                    d.deviceName = temp[0];
                    d.deviceIP = temp[1];
                    d.devicePort = temp[2];
                    d.id = name.GetHashCode();
                    onlineDevices.Add(d);
                    worker.ReportProgress(i * 10);
                }
            }
        }

        private void BWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.tbProgress.Text = (e.ProgressPercentage.ToString() + "%");
            devicesList.ItemsSource = onlineDevices;

        }

        private void BWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void AddManually(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddDevice_Manual.xaml", UriKind.Relative));
        }
    }
}